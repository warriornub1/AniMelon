import {deinflect, WordType, deinflectionKeys} from './deinfect.js';


let vocabs;
let vocabReadingsToIndexes;
let nameInfos;
let nameReadingToInfoIndex;
let abbreviations;
let kanjis;
let inited = false;

export async function init(callback){
    await fetch('./dict/vocabs.json').then(response => response.json()).then(json => {vocabs = json;});
    callback && callback(0.25);
    await fetch('./dict/vocab-reading-to-indexes.json').then(response => response.json()).then(json => {vocabReadingsToIndexes = json});
    callback && callback(0.5);
    await fetch("./dict/name-reading-infos.json").then(response => response.json()).then(json => {nameInfos = json});
    callback && callback(0.75);
    await fetch("./dict/name-reading-to-info-index.json").then(response => response.json()).then(json => {nameReadingToInfoIndex = json});
    callback && callback(1.00);
    await fetch("./dict/kanjis.json").then(response => response.json()).then(json => {kanjis = json});
    await fetch("./dict/abbreviations.json").then(response => response.json()).then(json => {abbreviations = json});
    console.log('dictionary initialised');
    inited = true
}

export function getSearchResult(searchResult) {
    if (!searchResult.searchTerm) {
        return "";
    }
    let html = "";
    if (searchResult.vocabs.length) {
        let entriesHTML = '';
        for (const vocabResult of searchResult.vocabs) {
            for (const matchEntry of vocabResult[2]) {
                let sensesHTML = '';
                let count = 1;
                for (const sense of matchEntry.senses) {
                    sensesHTML += `
                        <div>
                            <div class='search-result-vocab-entry-sense-glosses'>${count}. ${sense.glosses ? sense.glosses.join(', ') : ""}</div>
                        </div>
                    `;
                    count++;
                }
                let mainString = '';
                let kanaString = '';
                if (matchEntry.kanjiReadings && matchEntry.kanjiReadings.length) {
                    mainString = matchEntry.kanjiReadings[0];
                    if (matchEntry.kanaReadings && matchEntry.kanaReadings.length) {
                        kanaString = matchEntry.kanaReadings[0].k;
                    }
                } else {
                    if (matchEntry.kanaReadings && matchEntry.kanaReadings.length) {
                        mainString = matchEntry.kanaReadings[0].k;
                    }
                }


                entriesHTML += `
                        <div class='search-result-vocab-entry-header'>
                            <div>${mainString}  ${kanaString}</div>
                        </div>
                        <div class='search-result-vocab-entry-senses'>
                            ${sensesHTML}
                        </div>  
                `;
            }
        }


        html += `
            <div>
                <div class='search-result-vocab-entries'>
                    ${entriesHTML}
                </div>
            </div>
        `;
    }

    if (!html) {
        html = `
            <div class='search-result'>
                <div class='search-term'>&quot;${searchResult.searchTerm}&quot;</div>
                <div class='search-result-empty'>No results...</div>
            </div>
        `;
    } else {
        html = `
            <div class='search-result'>
                <div class='search-results'>${html}</div>
            </div>
        `;
    }

    return html;
}

export function searchWord(japanese){
    japanese = japanese.trim();
    const results = {names: [], vocabs: [], kanjis: [], searchTerm: japanese, matchedPart: japanese};
    if (!inited || !japanese){ 
        return results;
    }

    if (nameReadingToInfoIndex[japanese]){
        results.names = nameInfos[nameReadingToInfoIndex[japanese]];
    }

    for (const c of japanese){
        if (kanjis[c]){
            results.kanjis.push([c, kanjis[c]]);
        }
    }

    while(japanese.length){
        const candidates = deinflect(japanese);
        for (const [candidateIndex, candidate] of candidates.entries()) {
            let indexes = vocabReadingsToIndexes[candidate.word];
            if (!indexes){
                continue;
            }
            
            
            let sortedMatchesA = [];
            let sortedMatchesB = [];
            let matchingKanaReading;
            let allReadings = [];
            for (let index of indexes){
                const vocabEntry = {...vocabs[index]};//shallow copy
                if (vocabEntry.kanaReadings){
                    for (let kanaReading of vocabEntry.kanaReadings){
                        allReadings.push(kanaReading.k);
                        if (kanaReading.k === candidate.word){
                            matchingKanaReading = kanaReading;
                            if (kanaReading.restrictedToKebs){
                                vocabEntry.kanjiReadings = vocabEntry.kanjiReadings.filter(k => kanaReading.restrictedToKebs.includes(k));
                                if (!vocabEntry.kanjiReadings.length){
                                    delete vocabEntry.kanjiReadings;
                                }
                            }
                        }
                    }
                }
                if (vocabEntry.kanjiReadings){
                    allReadings = allReadings.concat(vocabEntry.kanjiReadings);
                }
                if (matchingKanaReading){
                    if (vocabEntry.kanaReadings[0] !== matchingKanaReading){
                        vocabEntry.kanaReadings = [matchingKanaReading].concat(vocabEntry.kanaReadings.filter(k=>k !== matchingKanaReading));
                    }
                } else {
                    if (vocabEntry.kanjiReadings[0] !== candidate.word){
                        vocabEntry.kanjiReadings = [candidate.word].concat(vocabEntry.kanjiReadings.filter(k=>k !== candidate.word));
                    }
                }

                let restrictedSenses = [];
                for (const sense of vocabEntry.senses){
                    if (!sense.onlyForReadings || sense.onlyForReadings.filter(k=>allReadings.includes(k)).length){
                        restrictedSenses.push(sense);
                    }
                }
                vocabEntry.senses = restrictedSenses;

                if (matchingKanaReading && !vocabEntry.kanjiReadings){
                    sortedMatchesA.push(vocabEntry);
                } else {
                    sortedMatchesB.push(vocabEntry);
                }     
                
            }


            let matches = sortedMatchesA.concat(sortedMatchesB);
            matches = matches.filter((match) =>candidateIndex === 0 || entryMatchesType(match, candidate.type));
            if (!matches.length) {
                continue;
            }
            
            let inflectionString;
            if (candidate.reasons.length) {
                inflectionString = '< ' + candidate.reasons.map((reasonList) => reasonList.map((reason) => deinflectionKeys[reason]).join(' < '));
            }
            results.vocabs.push([candidate.word, inflectionString, matches]);
        }
        //this means if we have a matching name, we give up after checking vocab for full word
        if (results.vocabs.length){
            break;
        } else if (results.names.length){
            break;
        } else {
            japanese = japanese.slice(0, -1);
            results.matchedPart = japanese;
        }
    }

    
    return results;
}





// Tests if a given entry matches the type of a generated deflection
//grabbed from https://github.com/birchill/10ten-ja-reader by Brian Birtles (see deinflect.js)
function entryMatchesType(entry, type) {
    const hasMatchingSense = test => entry.senses.some((sense) => sense.partsOfSentence?.some(test));

    if (
      type & WordType.IchidanVerb &&
      hasMatchingSense((pos) => pos.startsWith('v1'))
    ) {
      return true;
    }
  
    if (
      type & WordType.GodanVerb &&
      hasMatchingSense((pos) => pos.startsWith('v5') || pos.startsWith('v4'))
    ) {
      return true;
    }
  
    if (
      type & WordType.IAdj &&
      hasMatchingSense((pos) => pos.startsWith('adj-i'))
    ) {
      return true;
    }
  
    if (type & WordType.KuruVerb && hasMatchingSense((pos) => pos === 'vk')) {
      return true;
    }
  
    if (
      type & WordType.SuruVerb &&
      hasMatchingSense((pos) => pos.startsWith('vs-'))
    ) {
      return true;
    }
  
    if (type & WordType.NounVS && hasMatchingSense((pos) => pos === 'vs')) {
      return true;
    }
  
    return false;
  }



