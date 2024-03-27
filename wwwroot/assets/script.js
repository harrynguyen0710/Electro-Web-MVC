// document.addEventListener("DOMContentLoaded", function() {
//     const sliderContainer = document.querySelector('.slider-container');
//     const sliderItems = document.querySelectorAll('.slider-item');
//     const sliderWidth = sliderContainer.offsetWidth; // Get the width of the slider container
//     let currentIndex = 0;

//     function showSlide(index) {
//         const distance = -index * sliderWidth;
//         sliderContainer.style.transform = `translateX(${distance}px)`;
//         currentIndex = index;
//     }

//     function nextSlide() {
//         currentIndex = (currentIndex + 1) % sliderItems.length;
//         showSlide(currentIndex);
//     }

//     // Show the initial slide
//     showSlide(currentIndex);

//     // Automatically switch slides every 3 seconds
//     setInterval(nextSlide, 3000);
// });


function toggleProducts(category) {
    const newProducts = document.querySelector('.new-products');
    const bestSellers = document.querySelector('.best-sellers');

    if (category === 'new-products') {
        newProducts.style.display = 'block';
        bestSellers.style.display = 'none';
    } else if (category === 'best-sellers') {
        newProducts.style.display = 'none';
        bestSellers.style.display = 'block';
    }
}
