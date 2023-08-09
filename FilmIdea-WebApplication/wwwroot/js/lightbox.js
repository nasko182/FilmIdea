const photoItems = document.querySelectorAll('.photo-item');
const lightbox = document.createElement('div');
lightbox.classList.add('lightbox');
document.body.appendChild(lightbox);

photoItems.forEach(photoItem => {
    const img = photoItem.querySelector('img');
    img.addEventListener('click', () => {
        lightbox.innerHTML = `<img src="${img.src}" alt="Large Photo" />`;
        lightbox.classList.add('active');
    });
});

lightbox.addEventListener('click', () => {
    lightbox.classList.remove('active');
    lightbox.innerHTML = '';
});