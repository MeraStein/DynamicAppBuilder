
//returns coordinates of event.
window.getCoordinates = function (event) {
    const canvas = document.querySelector(".canvas");
    const canvasRect = canvas.getBoundingClientRect(); 

    return {
        x: event.clientX - canvasRect.left,
        y: event.clientY - canvasRect.top   
    };
};


//prevent default.
window.addEventListener('dragover', function (event) {
    event.preventDefault();
});

window.preventDefaultDrag = () => {
    event.preventDefault();
};

//
window.dragFunctions = {
    startDrag: function (element, index) {
        element.addEventListener('dragstart', function (e) {
            e.dataTransfer.setData('text/plain', index);
        });
    }
};

//
window.showConfirmation = (message) => {
    return new Promise((resolve) => {
        let result = confirm(message); 
        resolve(result); 
    });
};





