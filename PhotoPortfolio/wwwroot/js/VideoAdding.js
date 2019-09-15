var dragAndDropTarget = document.getElementById("VideoDropContainer");
var dragAndDropButton = document.getElementById("FileSubmitButton"  );
var uploadedFiles = [];

//Listeners for Drag&Drop{
    
    dragAndDropTarget.addEventListener("dragenter", function (event) {
        event.stopPropagation();
        event.preventDefault();
        
        document.getElementById("VideoDropContainer").style.backgroundColor = "#66bab6";
    }, false);

    dragAndDropTarget.addEventListener("dragover", function (event) {
        event.stopPropagation();
        event.preventDefault();
        
    }, false);

    dragAndDropTarget.addEventListener("dragleave", function (event) {
        event.stopPropagation();
        event.preventDefault();
        document.getElementById("VideoDropContainer").style.backgroundColor = "white";
}, false); 
dragAndDropTarget.addEventListener("drop", DropFile, false);

dragAndDropButton.addEventListener("click", function () { LoadFile(uploadedFiles, window.location.href + "/post", window.location.href + "/compile"),false})
// }
/**drop & load in local vaariable, also write name in h2 tag
@param event - event
**/
function DropFile(event) {
    document.getElementById("VideoDropContainer").style.backgroundColor = "white";
    event.stopPropagation();
    event.preventDefault();
    files = event.dataTransfer.files;
    len = files.length;

    //Loading in local variable
    for (var i = 0; i < len; i++) {

        console.log("Filename: " + files[i].name);
        console.log("Type: " + files[i].type.split("/")[0]);
        console.log("Widening: " + files[i].type.split("/")[1]);
        console.log("Size: " + files[i].size + " bytes");
        uploadedFiles.push(files[i]);
    }
    //Writing name in h2 tag
    var nameString = "";
    for (var i = 0; i < uploadedFiles.length; i++) {
        nameString += (i + 1) + "-" + "(" + "Filename: " + uploadedFiles[i].name + ") ";
    }
    document.getElementById("FilesInformation").textContent = nameString;
}