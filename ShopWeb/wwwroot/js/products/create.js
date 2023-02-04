window.addEventListener("load", (event) => {
    //console.log("Початок роботи");
    const myImages = document.getElementById("myImages");
    const files = document.getElementById("files");

    const imgPrev = document.getElementById("imgPrev");
    const imgCropper = document.getElementById("imgCropper");
    const cropper = new Cropper(imgCropper, {
        aspectRatio: 1 / 1,
        preview: imgPrev
    });

    files.onchange = function (e) {
        const list = e.target.files;
        //console.log("Select files", list);
        if (list && list.length) {
            for (let i = 0; i < list.length; i++) {
                const file = list[i];
                const url = URL.createObjectURL(file);
                const ModalCropper = document.getElementById("ModalCropper");
                var modal = bootstrap.Modal.getOrCreateInstance(ModalCropper);
                modal.show();
                cropper.replace(url);
            }
        }
        e.target.value = "";
    }

    const btnAdd = document.getElementById("btnAdd");
    btnAdd.onclick = function () {
        cropper.getCroppedCanvas().toBlob(async (blob) => {
            console.log("Cropper image", blob);

            const formData = new FormData();
            formData.append("file", blob);
            const resp = await axios.post("/Products/upload", formData,
                {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    }
                });
            console.log("resp server", resp);

            const model = resp.data;
            const url = `/images/${model.name}`;
            var data = `<img src ="${url}" height="100" />
                                <input type="hidden" name="images" value="${model.id}" />`;

            myImages.innerHTML += data;

            var modal = bootstrap.Modal.getOrCreateInstance(ModalCropper);
            modal.hide();
        });
    }


    let btnLeft = document.getElementById("btnLeft");
    btnLeft.onclick = function () {
        cropper.rotate(-90);
    }

    let btnRight = document.getElementById("btnRight");
    btnRight.onclick = function () {
        cropper.rotate(90);
    }
});
