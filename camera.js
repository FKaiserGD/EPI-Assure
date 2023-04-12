let currentCamera = 'environment'; // define a câmera atual como traseira

function toggleCamera() {
  const cameraStream = document.getElementById('camera-stream');

  // alterna a câmera atual
  currentCamera = currentCamera === 'user' ? 'environment' : 'user';

  // para o fluxo de vídeo atual
  cameraStream.srcObject.getTracks().forEach(track => {
    track.stop();
  });

  // inicia um novo fluxo de vídeo com a nova opção "facingMode"
  navigator.mediaDevices.getUserMedia({ video: { facingMode: { ideal: currentCamera } } })
    .then(function(stream) {
      cameraStream.srcObject = stream;
    })
    .catch(function(error) {
      console.error('Ocorreu um erro ao acessar a câmera:', error);
    });
}

function abreCamera() {
  const openCameraBtn = document.getElementById('open-camera-btn');
  const cameraStream = document.getElementById('camera-stream');

  openCameraBtn.addEventListener('click', function() {
    navigator.mediaDevices.getUserMedia({ video: { facingMode: 'user' } })
      .then(function(stream) {
        cameraStream.srcObject = stream;
      })
      .catch(function(error) {
        console.error('Ocorreu um erro ao acessar a câmera:', error);
      });
  });
}


function tiraFoto() {
  const picture = document.getElementById('picture');
  const cameraStream = document.getElementById('camera-stream');

  const canvas = document.createElement('canvas');
  canvas.width = cameraStream.videoWidth;
  canvas.height = cameraStream.videoHeight;
  canvas.getContext('2d').drawImage(cameraStream, 0, 0, canvas.width, canvas.height);

  const dataUrl = canvas.toDataURL('image/png');
  picture.setAttribute('src', dataUrl);
}

// adiciona os listeners após o carregamento completo do documento
window.onload = function() {
  const toggleCameraBtn = document.getElementById('toggle-camera-btn');
  toggleCameraBtn.addEventListener('click', toggleCamera);

  const takePictureBtn = document.getElementById('take-picture-btn');
  takePictureBtn.addEventListener('click', tiraFoto);

  const openCameraBtn = document.getElementById('open-camera-btn');
  openCameraBtn.addEventListener('click', abreCamera);
};
