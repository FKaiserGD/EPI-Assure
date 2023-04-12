function abreCamera() {
    const openCameraBtn = document.getElementById('open-camera-btn');
        const cameraStream = document.getElementById('camera-stream');
    
        openCameraBtn.addEventListener('click', function() {
          navigator.mediaDevices.getUserMedia({ video: true })
            .then(function(stream) {
              cameraStream.srcObject = stream;
            })
            .catch(function(error) {
              console.error('Ocorreu um erro ao acessar a câmera:', error);
            });
        });
}