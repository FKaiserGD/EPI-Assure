document.addEventListener("DOMContentLoaded", function() {
  if (!("mediaDevices" in navigator) || !("getUserMedia" in navigator.mediaDevices)) {
    alert("API de câmera não está disponível em seu navegador");
    return;
  }

  // get page elements
  const video = document.querySelector("#camera-stream");
  const toggleCameraBtn = document.querySelector("#open-camera-btn");
  const takePictureBtn = document.querySelector("#take-picture-btn");

  // video constraints
  let currentCamera = "user";
  const constraints = {
    video: {
      facingMode: currentCamera,
      width: { ideal: 640 },
      height: { ideal: 480 }
    }
  };

  // initialize
  async function initializeCamera() {
    if (video.srcObject) {
      const stream = video.srcObject;
      const tracks = stream.getTracks();

      tracks.forEach(function(track) {
        track.stop();
      });

      video.srcObject = null;
    }

    constraints.video.facingMode = currentCamera;

    try {
      const stream = await navigator.mediaDevices.getUserMedia(constraints);
      video.srcObject = stream;
    } catch (err) {
      alert("Não foi possível acessar a câmera");
    }
  }

  let useFrontCamera = true;

  function toggleCamera() {
    useFrontCamera = !useFrontCamera;
    currentCamera = useFrontCamera ? "user" : "environment";
    if (useFrontCamera) {
      video.style.transform = "scaleX(-1)";
    } else {
      video.style.transform = "none";
    }
    initializeCamera();
  }

  function takePicture() {
    const canvas = document.createElement("canvas");
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    const ctx = canvas.getContext("2d");

    if (useFrontCamera) {
      ctx.translate(canvas.width, 0);
      ctx.scale(-1, 1);
    }

    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);

    const picture = document.querySelector("#picture");
    picture.src = canvas.toDataURL("image/png");
    picture.style.display = "block";
  }

  function updateOrientation() {
    if (window.orientation == 90 || window.orientation == -90) {
      constraints.video.width = { ideal: 480 };
      constraints.video.height = { ideal: 640 };
    } else {
      constraints.video.width = { ideal: 640 };
      constraints.video.height = { ideal: 480 };
    }
    initializeCamera();
  }

  // handle events
  toggleCameraBtn.addEventListener("click", toggleCamera);
  takePictureBtn.addEventListener("click", takePicture);

  // initialize camera
  initializeCamera();

  // update camera orientation on device orientation change
  window.addEventListener("orientationchange", updateOrientation);
});
