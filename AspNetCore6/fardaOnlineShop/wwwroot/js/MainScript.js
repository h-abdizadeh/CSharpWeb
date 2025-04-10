function GetImg(input) {
    if (input.files && input.files[0]) {
        let reader = new FileReader();
        reader.onload = (e) => {
            let img = document.getElementById('img-view')
            img.setAttribute('src', e.target.result)
        }

        reader.readAsDataURL(input.files[0]);
    }
}