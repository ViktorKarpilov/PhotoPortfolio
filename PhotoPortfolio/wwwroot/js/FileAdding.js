
/**
 * 
 * @param {Array} files - array of files
 */
function LoadFile(files, path,compilePath) {

    
    //Compact in data for sending
    for (var i = 0; i < files.length; i++) {
        sendUsingForm(files[i], path, compilePath);
        
    }

}

function getFileData(file, callback) {
    const fr = new FileReader();
    fr.addEventListener('load', (e) => {
        callback(e.target.result);
        
    });
    fr.readAsArrayBuffer(file);
}

function compileLoadedFiles(name, lenth,compilePath) {
    let form = new FormData();
    form.append('name', name);
    form.append('lenth', lenth);
    console.log(form);
    fetch(compilePath, {
        method: 'post',
        body: form
    })
        .then(res => res.status)
        .then(res => console.log(res))
        .catch(err => console.error(err));
    console.log("Files loaded");
}

function sendUsingForm(file, path,compilePath) {
    console.log(file);
    let blobs = sliceFile(file, Math.floor(file.size / 80000)+2);
    getFileData(file, (fileData) => {
        for (var i = 0; i < blobs.length; i++) {

            fileData = blobs[i];
            let form = new FormData();
            form.append('name', file.name);
            form.append('id', i); 
            let data = new Blob([fileData], { type: 'application/octet-binary' });
            form.append('data', data); // this is assumed to be Blob or File objects

            var counter = 0;
            fetch(path, {
                method: 'post',
                body: form
            })
                .then(res => res.json())
                .then(function () {
                    if (counter == String(blobs.length-1)) {
                        compileLoadedFiles(file.name, blobs.length, compilePath);
                    }
                    counter += 1;
                })
                .catch(err => console.error(err));
        }
        
        
    });
    
}

/**
* @param {File|Blob} - file to slice
* @param {Number} - chunksAmount
* @return {Array} - an array of Blobs
**/
function sliceFile(file, chunksAmount) {
    var byteIndex = 0;
    var chunks = [];

    for (var i = 0; i < chunksAmount; i += 1) {
        var byteEnd = Math.ceil((file.size / chunksAmount) * (i + 1));
        chunks.push(file.slice(byteIndex, byteEnd));
        byteIndex += (byteEnd - byteIndex);
    }

    return chunks;
}