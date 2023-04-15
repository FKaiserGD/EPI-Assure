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
      // apply horizontal flip to the canvas context
      ctx.translate(canvas.width, 0);
      ctx.scale(-1, 1);
    }

    // create a temporary canvas to apply the horizontal flip to the camera image
    const tmpCanvas = document.createElement("canvas");
    tmpCanvas.width = video.videoWidth;
    tmpCanvas.height = video.videoHeight;
    const tmpCtx = tmpCanvas.getContext("2d");
    tmpCtx.translate(video.videoWidth, 0);
    tmpCtx.scale(-1, 1);
    tmpCtx.drawImage(video, 0, 0, video.videoWidth, video.videoHeight);

    // draw the flipped camera image onto the main canvas
    ctx.drawImage(tmpCanvas, 0, 0, video.videoWidth, video.videoHeight);

    const picture = document.querySelector("#picture");
    picture.src = canvas.toDataURL("image/png");
    picture.style.display = "block";
  }

  // handle events
  toggleCameraBtn.addEventListener("click", toggleCamera);
  takePictureBtn.addEventListener("click", takePicture);

  // initialize camera
  initializeCamera();
});
