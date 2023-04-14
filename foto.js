function tiraFoto() {
    const takePictureBtn = document.getElementById('take-picture-btn');
    const picture = document.getElementById('picture');

    takePictureBtn.addEventListener('click', function() {
        const canvas = document.createElement('canvas');
        canvas.width = cameraStream.videoWidth;
        canvas.height = cameraStream.videoHeight;
        canvas.getContext('2d').drawImage(cameraStream, 0, 0, canvas.width, canvas.height);
        const dataUrl = canvas.toDataURL('image/png');
        picture.setAttribute('src', dataUrl);
        picture.style.display = "block";
    });
}