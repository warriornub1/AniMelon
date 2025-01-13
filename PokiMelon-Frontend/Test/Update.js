const url = "http://localhost:5087/Test/";

const singleData = {
    "id": 1,
    "a": "Updated",
    "b": "Updated",
    "c": "Updated",
    "d": "Updated",
    "e": "Updated",
    "f": "Updated",
    "g": "Updated",
    "h": "Updated",
    "i": "Updated",
    "j": "Updated",
    "k": "Updated",
    "l": "Updated",
    "m": "Updated",
    "n": "Updated"
}

const multipleData = [];
for (let i = 0; i < 6; i++) {
    singleData.id = i + 1;
    multipleData.push(JSON.parse(JSON.stringify(singleData))); // Deep clone
}


const id = 2;
const patchOperations = [
    { "op": "replace", "path": "/a", "value": "asdasdas" },
    { "op": "replace", "path": "/b", "value": "asds" }
];

const  patchMultipleOperation =          [
    {
        "id": 1,
        "patchDocument": [
            { "op": "replace", "path": "/a", "value": "Patch Multiple" },
            { "op": "replace", "path": "/b", "value": "Patch Multiple" }
        ]
    },
    {
        "id": 2,
        "patchDocument": [
            { "op": "replace", "path": "/a", "value": "Patch Multiple" },
            { "op": "replace", "path": "/b", "value": "Patch Multiple" }
        ]
    },
    {
        "id": 3,
        "patchDocument": [
            { "op": "replace", "path": "/a", "value": "Patch Multiple" },
            { "op": "replace", "path": "/b", "value": "Patch Multiple" }
        ]
    },
    {
        "id": 4,
        "patchDocument": [
            { "op": "replace", "path": "/c", "value": "Patch Multiple" },
            { "op": "replace", "path": "/d", "value": "Patch Multiple" }
        ]
    },
    {
        "id": 5,
        "patchDocument": [
            { "op": "replace", "path": "/a", "value": "Patch Multiple" },
            { "op": "replace", "path": "/b", "value": "Patch Multiple" }
        ]
    },
    {
        "id": 6,
        "patchDocument": [
            { "op": "replace", "path": "/c", "value": "Patch Multiple" },
            { "op": "replace", "path": "/d", "value": "Patch Multiple" }
        ]
    }
]


const UpdateTestSingle = async() => {
    try{
        const res = await fetch(url + "UpdateTest", {
            method: 'PUT',
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify(singleData)
        })
        .then(res => {
            if(!res.ok){
                console.log(`Error: ${res.status} ${res.statusText}`);
                return;
            }
            return res;
        })
    .then(data => {
        console.log("Put Success");
    })
    } catch(error){
        console.error("An error occurred:", error);
    }
}

const UpdateTestPatch = async() => {
    try{
        const res = await fetch(url + `UpdateTestPatch/${id}`, {
            method: "PATCH",
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify(patchOperations)
        })
        .then(res => {
            if(!res.ok){
                console.log(`Error: ${res.status} ${res.statusText}`);
                return;
            }
            return res;
        })
    .then(data => {
        console.log("Patch Success");
    })
    } catch(error){
        console.error("An error occurred:", error);
    }
}


const UpdateMultipleTest = async() => {
    try{
        const res = await fetch(url + `UpdateTestMultiple`, {
            method: "PUT",
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify(multipleData)
        })
        .then(res => {
            if(!res.ok){
                console.log(`Error: ${res.status} ${res.statusText}`);
                return;
            }
            return res;
        })
    .then(data => {
        console.log("Update Multiple Success");
    })
    } catch(error){
        console.error("An error occurred:", error);
    }
}

const UpdateMultipleTestPatch = async() => {
    try{
        const res = await fetch(url + `UpdateMultiplePatch`, {
            method: "PATCH",
            headers: {
                'Content-Type' : 'application/json'
            },
            body: JSON.stringify(patchMultipleOperation)
        })
        .then(res => {
            if(!res.ok){
                console.log(`Error: ${res.status} ${res.statusText}`);
                return;
            }
            return res;
        })
    .then(data => {
        console.log("Patch Multiple Success");
    })
    } catch(error){
        console.error("An error occurred:", error);
    }
}

function measureExecutionTime(func, funcName) {
    console.time(funcName);
    func();
    console.timeEnd(funcName);
}

measureExecutionTime(UpdateTestSingle, 'UpdateTestSingle');
measureExecutionTime(UpdateTestPatch, 'UpdateTestPatch');
measureExecutionTime(UpdateMultipleTest, 'UpdateMultipleTest');
measureExecutionTime(UpdateMultipleTestPatch, 'UpdateMultipleTestPatch');