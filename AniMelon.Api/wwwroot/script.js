import { init, getSearchResult, searchWord } from './search.js';
import { getDeinflectRuleGroups, deinflect } from './deinfect.js';

const fileInput = document.getElementById('fileInput');
const uploadButton = document.getElementById('customButton');
const statusDiv = document.getElementById('status');
const errorDiv = document.getElementById('error');
const videoPlayerWrapper = document.getElementById('videoPlayerWrapper');
const skipBackward = document.querySelector(".skip-backward i");
const skipForward = document.querySelector(".skip-forward i");
const currentVidTime = document.querySelector(".current-time");
const videoDuration = document.querySelector(".video-duration");
const videoTimeLine = document.querySelector(".video-timeline");
const progressBar = document.querySelector(".progress-bar");
const subtitleSelect = document.getElementById('subtitle-select');
const volumeBtn = document.querySelector(".volume i");
const volumeSlider = document.querySelector(".left input");
const playPauseBtn = document.querySelector(".play-pause i");
const ccButton = document.getElementById('ccButton');
const subtitleFileInput = document.getElementById('subtitleFileInput');
const subtitlesPanel = document.getElementById('subtitlesPanel');
const subtitle1 = document.getElementById('subtitle-1');
const subtitle2 = document.getElementById('subtitle-2');
const video = document.getElementById('video');
const card = document.querySelector('.card');
const definitionCard = document.querySelector('.definition-card');
const closeButton = document.querySelector('#closeButton');

let duration = 0;
const sub1Map = new Map();
const sub2Map = new Map();
const segmenter = new TinySegmenter();

let sub1 = [], sub2 = [];

const player = videojs('video', {
    controls: false,
    autoplay: false,
    preload: 'auto',
    liveui: true, // Enable live UI for dynamic HLS
});

await init((progress) => {
    console.log(`Loading progress: ${progress * 100}%`);
});

// subtitle buttons
let subtitleTrack = null;
ccButton.addEventListener('click', () => {
    if (!subtitleTrack)
        subtitleFileInput.click();
    else {
        const isVisible = subtitleTrack.mode === "showing";
        subtitleTrack.mode = isVisible ? "hidden" : "showing";
        ccButton.setAttribute("aria-pressed", String(!isVisible));
    }
});

subtitleFileInput.addEventListener('change', (e) => {
    const file = event.target.files[0];
    if (!file) return;

    const reader = new FileReader();

    reader.onload = function (e) {
        const fileText = e.target.result;

        if (file.name.endsWith('.srt')) {
            sub2 = parseSRT(fileText);
            sub2.forEach((sub, i) => {
                for (let t = Math.floor(sub.start); t <= Math.floor(sub.end); t++) {
                    sub2Map.set(t, i);
                }
            });
        } else if (file.name.endsWith('.vtt')) {
            sub2 = parseVTT(fileText);
            sub2.forEach((sub, i) => {
                for (let t = Math.floor(sub.start); t <= Math.floor(sub.end); t++) {
                    sub2Map.set(t, i);
                }
            });
        } else {
            alert("Unsupported file type. Please upload .srt or .vtt only.");
            return;
        }

        renderSidebar();
    };

    reader.readAsText(file);
})

// move time line
player.on('loadeddata', function () {
    videoTimeLine.style.display = 'block';
    videoDuration.innerText = formatTime(duration);
    console.log('Duration after metadata loaded:', duration);
});

videoTimeLine.addEventListener("mousemove", e => {
    let timelineWidth = videoTimeLine.clientWidth;
    let offsetX = e.offsetX;
    let percent = Math.floor((offsetX / timelineWidth) * duration);
    const progressTime = videoTimeLine.querySelector("span");
    offsetX = offsetX < 20 ? 20 : (offsetX > timelineWidth - 20) ? timelineWidth - 20 : offsetX;
    progressTime.style.left = `${offsetX}px`;
    progressTime.innerText = formatTime(percent);
});

videoTimeLine.addEventListener("click", e => {
    const timelineWidth = videoTimeLine.clientWidth;
    const clickPosition = e.offsetX;
    const newTime = (clickPosition / timelineWidth) * duration;
    player.currentTime(newTime);
});

document.addEventListener("keydown", (e) => {
    const { code, key } = e;

    // Normalize key for case-insensitivity for 'c'
    const keyLower = key.toLowerCase();

    switch (code) {
        case "Space":
            e.preventDefault();
            if (player.paused()) {
                player.play();
            } else {
                player.pause();
            }
            break;

        case "ArrowLeft":
            e.preventDefault();
            player.currentTime(player.currentTime() - 15);
            break;

        case "ArrowRight":
            e.preventDefault();
            player.currentTime(player.currentTime() + 15);
            break;

        case "Escape":
            e.preventDefault();
            if (card.innerHTML.trim() !== "") {
                definitionCard.classList.remove('active');
                card.innerHTML = "";
            }
            break;

        default:
            //if (keyLower === "c") {
            //    e.preventDefault();
            //    ccButton.click();
            //}
            break;
    }
});

closeButton.addEventListener("click", () => {
    definitionCard.classList.remove('active');
    card.innerHTML = "";
});

video.addEventListener("click", () => {
    if (player.paused()) {
        player.play();
    } else {
        player.pause();
    }
});

skipBackward.addEventListener("click", () => player.currentTime(player.currentTime() - 15) );
skipForward.addEventListener("click", () => player.currentTime(player.currentTime() + 15) );

player.on("play", () => playPauseBtn.classList.replace("fa-play", "fa-pause"));
player.on("pause", () => playPauseBtn.classList.replace("fa-pause", "fa-play"));

playPauseBtn.addEventListener("click", () => {
    console.log("clicked")
    if (player.paused()) {
        player.play();
    } else {
        player.pause();
    }
});

uploadButton.addEventListener('click', () => {
    fileInput.click(); // Trigger the file input
});

fileInput.addEventListener('change', async function () {
    const file = fileInput.files[0];
    if (!file) {
        errorDiv.textContent = 'Please select an MKV file.';
        return;
    }

    //statusDiv.textContent = 'Uploading file...';
    //errorDiv.textContent = '';

    const formData = new FormData();
    formData.append('file', file);

    try {
        const response = await fetch('https://localhost:7082/video/upload', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) {
            throw new Error('Upload failed: ' + (await response.text()));
        }


        const data = await response.json();

        await waitForPlaylist("https://localhost:7082" + data.hlsUrl);

        if (data.subtitles && data.subtitles.length > 0) {
            data.subtitles.forEach(sub => {
                const option = document.createElement('option');
                option.value = sub.index;
                option.text = `${sub.title} (${sub.language})`;
                subtitleSelect.appendChild(option);
            });
        } else {
            subtitleSelect.innerHTML = '<option value="none">No subtitles available</option>';
        }

        duration = durationToSeconds(data.duration)
        // Set the HLS source for the player
        player.src({
            src: data.hlsUrl,
            type: 'application/x-mpegURL'
        });
        player.play();
    } catch (error) {
        errorDiv.textContent = 'Error: ' + error.message;
        statusDiv.textContent = '';
    }
} )

subtitleSelect.addEventListener("change", async function () {
    const selectedOption = this.options[this.selectedIndex];

    if (selectedOption.value === "none") return;


    const subtitleData = {
        index: parseInt(selectedOption.value),
        title: selectedOption.text
    };

    async function fetchSubtitlePath() {
        try {
            const response = await fetch('https://localhost:7082/Video/ExtractSubtitle', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(subtitleData)
            });

            return await response.text();  // Assign response body to variable


        } catch (error) {
            console.log(error);
        }
    }


    const path = await fetchSubtitlePath();
    var response = await fetch(path);
    if (!response.ok) throw new Error('Subtitle file not found.');

    const fileText = await response.text();
    const fileName = path.split('/').pop();


    if (fileName.endsWith('.srt')) {
        sub1 = parseSRT(fileText);
        sub1.forEach(sub => {
            for (let t = Math.floor(sub.start); t <= Math.floor(sub.end); t++) {
                sub1Map.set(t, sub);
            }
        });
    } else if (fileName.endsWith('.vtt')) {
        sub1 = parseVTT(fileText);
        sub1.forEach(sub => {
            for (let t = Math.floor(sub.start); t <= Math.floor(sub.end); t++) {
                sub1Map.set(t, sub);
            }
        });
    } else {
        alert("Unsupported file type. Please upload .srt or .vtt only.");
        return;
    }

    //renderSidebar(sub1);
});

const formatTime = time => {
    let seconds = Math.floor(time % 60),
        minutes = Math.floor(time / 60) % 60,
        hours = Math.floor(time / 3600);
    seconds = seconds < 10 ? `0${seconds}` : seconds;
    minutes = minutes < 10 ? `0${minutes}` : minutes;
    hours = hours < 10 ? `0${hours}` : hours;
    if (hours == 0) {
        return `${minutes}:${seconds}`
    }
    return `${hours}:${minutes}:${seconds}`;
}

// Volume Button
let prevVolume;
player.volume (0.5);
volumeBtn.addEventListener("click", () => {
    if (!volumeBtn.classList.contains("fa-volume-high")) {
        player.volume(prevVolume);
        volumeBtn.classList.replace("fa-volume-xmark", "fa-volume-high");
    }
    else {
        // set to mute
        prevVolume = player.volume();
        player.volume(0.0);
        volumeBtn.classList.replace("fa-volume-high", "fa-volume-xmark");
    }
    volumeSlider.value = player.volume();
});

volumeSlider.addEventListener("input", e => {
    player.volume(e.target.value);
    if (e.target.value == 0)
        return volumeBtn.classList.replace("fa-volume-high", "fa-volume-xmark");

    volumeBtn.classList.replace("fa-volume-xmark", "fa-volume-high");

});

//let prevBlock;
//let lastSubTwoText = '';
//const ruleGroups = getDeinflectRuleGroups();

//player.on('timeupdate', e => {

//    const currentTime = player.currentTime();
//    const t = Math.floor(currentTime); 
//    const percent = (currentTime / duration) * 100;

//    progressBar.style.width = `${percent}%`;
//    currentVidTime.innerText = formatTime(currentTime);

//    // Update subtitle1
//    const subOne = sub1Map.get(t);
//    if (subtitle1.innerHTML !== (subOne?.text || '')) {
//        subtitle1.innerHTML = subOne?.text || '';
//    }

//    // Update subtitle2
//    const subTwoIndex = sub2Map.get(t);
//    const subTwo = subTwoIndex !== undefined ? sub2[subTwoIndex] : null;
//    const newText = subTwo?.text || '';

//    if (newText !== lastSubTwoText) {
//        lastSubTwoText = newText;
//        subtitle2.innerHTML = ''; // Clear previous words

//        if (newText) {
//            const words = segmenter.segment(newText);

//            for (const word of words) {
//                const span = document.createElement("span");
//                span.textContent = word;
//                span.className = "word";
//                span.onclick = function() {
//                    const searchResult = getSearchResult(searchWord(word));
//                    definitionCard.classList.add('active');
//                    card.innerHTML = searchResult
//                };
//                subtitle2.appendChild(span);
//            }
//        }
//    }

//    for (let i = 0; i < subtitlesPanel.children.length; i++) {
//        const block = subtitlesPanel.children[i];

//        if (sub2.length > 0 && currentTime >= sub2[i].start && currentTime <= sub2[i].end) {

//            if (prevBlock !== undefined && prevBlock !== block) {
//                prevBlock.classList.remove('active');
//            }

//            prevBlock = block
//            block.classList.add('active');
//            block.scrollIntoView({ behavior: 'smooth', block: 'center' });
//            break;
//        }
//        //else {
//        //    block.classList.remove('active');
//        //}
//    }

//});

let lastSecond = -1;
let lastSubTwoText = '';
let prevActiveBlock = null;

player.on('timeupdate', e => {
    const currentTime = player.currentTime();
    const t = Math.floor(currentTime); // integer seconds

    // Only update when the second changes
    if (t === lastSecond) return;
    lastSecond = t;

    // Update progress bar
    const percent = (currentTime / duration) * 100;
    progressBar.style.width = `${percent}%`;

    // Update current time display
    currentVidTime.innerText = formatTime(currentTime);

    // Update subtitle1
    const subOne = sub1Map.get(t);
    const newSub1Text = subOne?.text || '';
    if (subtitle1.innerHTML !== newSub1Text) {
        subtitle1.innerHTML = newSub1Text;
    }

    // Update subtitle2
    const subTwoIndex = sub2Map.get(t);
    const subTwo = subTwoIndex !== undefined ? sub2[subTwoIndex] : null;
    const newSub2Text = subTwo?.text || '';

    if (newSub2Text !== lastSubTwoText) {
        lastSubTwoText = newSub2Text;
        subtitle2.innerHTML = ''; // Clear previous words

        if (newSub2Text) {
            const words = segmenter.segment(newSub2Text);
            for (const word of words) {
                const span = document.createElement("span");
                span.textContent = word;
                span.className = "word";
                span.onclick = () => {
                    const searchResult = getSearchResult(searchWord(word));
                    definitionCard.classList.add('active');
                    card.innerHTML = searchResult;
                };
                subtitle2.appendChild(span);
            }
        }
    }

    // Update subtitles panel
    if (sub2.length > 0) {
        const activeIndex = sub2.findIndex(s => currentTime >= s.start && currentTime <= s.end);
        if (activeIndex !== -1) {
            const activeBlock = subtitlesPanel.children[activeIndex];
            if (prevActiveBlock && prevActiveBlock !== activeBlock) {
                prevActiveBlock.classList.remove('active');
            }
            if (activeBlock && prevActiveBlock !== activeBlock) {
                activeBlock.classList.add('active');
                activeBlock.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }
            prevActiveBlock = activeBlock;
        }
    }
});

function renderSidebar() {
    subtitlesPanel.innerHTML = '';

    if (sub2.length > 0) {

        for (let i = 0; i < sub2.length; i++) {
            const block = document.createElement('div');
            block.className = 'subtitle-block';
            block.dataset.index = i;

            const en = document.createElement('div');
            en.className = 'subtitle-sub2';
            en.innerHTML = sub2[i].text;

            block.appendChild(en);

            block.addEventListener('click', () => {
                player.currentTime(sub2[i].start);
                player.play();
            });

            subtitlesPanel.appendChild(block);
        }

    }
}

function extractSubtitleFromTags(subtitle) {
    let asd = subtitle.replace(/<\/i>\s*<i>/g, '</i>\n<i>');
    return asd.replace(/<\/?[^>]+(>|$)/g, "");
}

function toSeconds(time) {
    const [h, m, s] = time.split(':');
    const [sec, ms] = s.split('.');
    return +h * 3600 + +m * 60 + +sec + (+ms || 0) / 1000;
}

function toSecondsSRT(timeStr) {
    const [hours, minutes, rest] = timeStr.split(':');
    const [seconds, milliseconds] = rest.split(',');
    return (
        parseInt(hours) * 3600 +
        parseInt(minutes) * 60 +
        parseInt(seconds) +
        parseInt(milliseconds) / 1000
    );
}

function parseVTT(data) {
    const lines = data.split('\n').filter(line => line.trim() !== '');
    const results = [];

    for (let i = 0; i < lines.length; i++) {
        if (lines[i].includes('-->')) {
            const [start, end] = lines[i].split(' --> ');
            const text = lines[i + 1];
            results.push({
                start: toSeconds(start),
                end: toSeconds(end),
                text
            });
            i++;
        }
    }

    return results;
}

function parseSRT(data) {
    const blocks = data.split(/\r?\n\r?\n/); // Split by empty lines
    const results = [];

    for (const block of blocks) {
        const lines = block.split(/\r?\n/).filter(Boolean);

        if (lines.length >= 2) {
            // First line: index (can be ignored)
            // Second line: timecodes
            const timeMatch = lines[1].match(/(\d{2}:\d{2}:\d{2},\d{3})\s-->\s(\d{2}:\d{2}:\d{2},\d{3})/);
            if (!timeMatch) continue;

            const [_, start, end] = timeMatch;

            // Join the rest of the lines as the subtitle text
            const text = extractSubtitleFromTags(lines.slice(2).join('\n'));

            results.push({
                start: toSecondsSRT(start),
                end: toSecondsSRT(end),
                text
            });
        }
    }

    return results;
}

function durationToSeconds(duration) {
    const [hours, minutes, seconds] = duration.split(':').map(Number);
    return hours * 3600 + minutes * 60 + seconds;
}

async function waitForPlaylist(hlsUrl, retries = 10, delay = 1000) {
    for (let i = 0; i < retries; i++) {
        try {
            const response = await fetch(hlsUrl);
            if (response.ok) {
                return; // Playlist is available
            }
        } catch {
            // Ignore fetch errors and retry
        }
        await new Promise(resolve => setTimeout(resolve, delay));
    }
    throw new Error('HLS playlist not available after retries.');
}


