(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner(0);


    // Fixed Navbar
    $(window).scroll(function () {
        if ($(window).width() < 992) {
            if ($(this).scrollTop() > 55) {
                $('.fixed-top').addClass('shadow');
            } else {
                $('.fixed-top').removeClass('shadow');
            }
        } else {
            if ($(this).scrollTop() > 55) {
                $('.fixed-top').addClass('shadow').css('top', -55);
            } else {
                $('.fixed-top').removeClass('shadow').css('top', 0);
            }
        } 
    });
    
    
   // Back to top button
   $(window).scroll(function () {
    if ($(this).scrollTop() > 300) {
        $('.back-to-top').fadeIn('slow');
    } else {
        $('.back-to-top').fadeOut('slow');
    }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Testimonial carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 2000,
        center: false,
        dots: true,
        loop: true,
        margin: 25,
        nav : true,
        navText : [
            '<i class="bi bi-arrow-left"></i>',
            '<i class="bi bi-arrow-right"></i>'
        ],
        responsiveClass: true,
        responsive: {
            0:{
                items:1
            },
            576:{
                items:1
            },
            768:{
                items:1
            },
            992:{
                items:2
            },
            1200:{
                items:2
            }
        }
    });


    // vegetable carousel
    $(".vegetable-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1500,
        center: false,
        dots: true,
        loop: true,
        margin: 25,
        nav : true,
        navText : [
            '<i class="bi bi-arrow-left"></i>',
            '<i class="bi bi-arrow-right"></i>'
        ],
        responsiveClass: true,
        responsive: {
            0:{
                items:1
            },
            576:{
                items:1
            },
            768:{
                items:2
            },
            992:{
                items:3
            },
            1200:{
                items:4
            }
        }
    });


    // Modal Video
    $(document).ready(function () {
        var $videoSrc;
        $('.btn-play').click(function () {
            $videoSrc = $(this).data("src");
        });
        console.log($videoSrc);

        $('#videoModal').on('shown.bs.modal', function (e) {
            $("#video").attr('src', $videoSrc + "?autoplay=1&amp;modestbranding=1&amp;showinfo=0");
        })

        $('#videoModal').on('hide.bs.modal', function (e) {
            $("#video").attr('src', $videoSrc);
        })
    });



    // Product Quantity
    $('.quantity button').on('click', function () {
        var button = $(this);
        var oldValue = button.parent().parent().find('input').val();
        if (button.hasClass('btn-plus')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        button.parent().parent().find('input').val(newVal);
    });

})(jQuery);
//Sự kiện click màu sắc và size
document.querySelectorAll('.color-circle').forEach(circle => {
    circle.addEventListener('click', function() {
        // Gỡ bỏ class 'selected' từ tất cả màu sắc
        document.querySelectorAll('.color-circle').forEach(c => c.classList.remove('selected'));
        // Thêm class 'selected' cho màu được chọn
        this.classList.add('selected');
    });
});

document.querySelectorAll('.size-circle').forEach(size => {
    size.addEventListener('click', function() {
        // Gỡ bỏ class 'selected' từ tất cả kích thước
        document.querySelectorAll('.size-circle').forEach(s => s.classList.remove('selected'));
        // Thêm class 'selected' cho kích thước được chọn
        this.classList.add('selected');
    });
});

//Sự kiện sang trang với hiện thị tối đa số sản phẩm trong 1 trang
// const productsPerPage = 3; // Số sản phẩm hiển thị trên mỗi trang
// let currentPage = 1;

// function displayProducts(page) {
//     const startIndex = (page - 1) * productsPerPage;
//     const endIndex = startIndex + productsPerPage;
//     const currentProducts = products.slice(startIndex, endIndex);
    
//     const productContainer = document.getElementById('product-container');
//     productContainer.innerHTML = '';

//     currentProducts.forEach(product => {
//         const colorsToShow = product.colors.slice(0, 3);
//         const colorsHTML = colorsToShow.map(color => `<div class="color-circle bg-${color} rounded-circle me-2" style="width: 20px; height: 20px;"></div>`).join('');

//         productContainer.innerHTML += `
//             <div class="col-md-6 col-lg-4 col-xl-3 product-item" data-category="Sneakers">
//                 <div class="shadow-sm border-0">
//                     <div class="product-image position-relative">
//                         <img src="${product.img}" class="img-fluid w-100 rounded-top" alt="${product.name}" style="transition: transform 0.3s ease;">
//                         <div class="discount-badge position-absolute bg-danger text-white rounded-pill px-2" style="top: 15px; left: 15px; font-size: 0.85rem;">-20%</div>
//                     </div>
//                     <div class="product-content p-3 bg-white rounded-bottom">
//                         <div class="mb-2">
//                             <div class="d-flex mt-1">${colorsHTML}</div>
//                         </div>
//                         <h5 class="mb-2 text-primary">${product.name}</h5>
//                         <p class="text-muted mb-2">Sneakers</p>
//                         <div class="size-options mb-2">
//                             <div class="d-flex mt-1">
//                                 <div class="size-circle rounded-circle me-2" data-size="40">40</div>
//                                 <div class="size-circle rounded-circle me-2" data-size="41">41</div>
//                                 <div class="size-circle rounded-circle me-2" data-size="42">42</div>
//                                 <div class="size-circle rounded-circle me-2" data-size="43">43</div>
//                                 <div class="size-circle rounded-circle me-2" data-size="44">44</div>
//                             </div>
//                         </div>
//                         <div class="d-flex justify-content-between align-items-center">
//                             <p class="fs-5 text-success fw-bold mb-0">$${product.price.toFixed(2)}</p>
//                             <div class="action-icons">
//                                 <a href="#" class="text-primary me-2 add-to-cart">
//                                     <i class="fa fa-shopping-cart"></i>
//                                 </a>
//                                 <a href="#" class="text-secondary view-details">
//                                     <i class="fa fa-info-circle"></i>
//                                 </a>
//                             </div>
//                         </div>
//                     </div>
//                 </div>
//             </div>
//         `;
//     });

//     // Cập nhật thông tin trang
//     document.getElementById('page-info').innerText = `Page ${page} of ${Math.ceil(products.length / productsPerPage)}`;
// }

// function changePage(page) {
//     const totalPages = Math.ceil(products.length / productsPerPage);
//     if (page < 1 || page > totalPages) return; // Giới hạn trang
//     currentPage = page;
//     displayProducts(currentPage);
// }

// // Hiển thị sản phẩm trang đầu tiên khi tải trang
// displayProducts(currentPage);


document.querySelectorAll('.product-card').forEach(product => {
    const originalPriceElement = product.querySelector('.original-price');
    const salePriceElement = product.querySelector('.sale-price');

    if (!originalPriceElement) {
        console.error('Không tìm thấy .original-price trong', product);
        return;
    }

    const originalPrice = parseFloat(originalPriceElement.textContent.replace('$', ''));
    if (isNaN(originalPrice)) {
        console.error('Giá gốc không hợp lệ:', originalPriceElement.textContent);
        return;
    }
    
    const discountBadge = product.querySelector('.discount-badge');
    if (discountBadge) {
        const discountText = discountBadge.textContent.replace('-', '').replace('%', '');
        const discountPercentage = parseFloat(discountText);
        if (isNaN(discountPercentage)) {
            console.error('Tỷ lệ giảm giá không hợp lệ:', discountText);
            return;
        }

        const salePrice = originalPrice - (originalPrice * (discountPercentage / 100));
        if (salePriceElement) {
            salePriceElement.textContent = `$${salePrice.toFixed(2)}`;
            salePriceElement.style.display = 'block';
        }
        originalPriceElement.style.color = '#ddd';
        originalPriceElement.style.textDecoration = 'line-through';
    } else {
        originalPriceElement.style.textDecoration = 'none';
        if (salePriceElement) {
            salePriceElement.style.display = 'none';
        }
    }
});

document.getElementById('minPrice').addEventListener('input', function() {
    var minValue = this.value;
    var maxValue = this.max;
    document.getElementById('priceDisplay').innerText = minValue.toLocaleString('vi-VN') + " VND - " + maxValue.toLocaleString('vi-VN') + " VND";
});

// Fake Data Generator for Products
const products = [];
const brands = ['Nike', 'Adidas', 'Puma'];
const shoeTypes = ['Sneaker', 'Boot', 'Sandal'];
const materials = ['Da', 'Vải'];
const colors = ['white', 'black', 'blue'];
const soles = ['Cao su', 'Bọt'];
const sizes = [40, 41, 42, 43, 44];

// Hàm để tạo dữ liệu giả
function getRandomElement(array) {
    return array[Math.floor(Math.random() * array.length)];
}

function generateRandomPrice(min, max) {
    return (Math.random() * (max - min) + min).toFixed(2); // Thay đổi để có giá thập phân
}

function generateFakeProducts(count = 50) {
    for (let i = 0; i < count; i++) {
        const originalPrice = generateRandomPrice(500000, 5000000);
        const salePrice = Math.random() > 0.5 ? generateRandomPrice(500000, originalPrice) : null; // Giá khuyến mãi phải nhỏ hơn giá gốc
        const salePercentage = salePrice ? ((originalPrice - salePrice) / originalPrice * 100).toFixed(0) : null;

        products.push({
            id: i + 1,
            name: `Sản phẩm ${i + 1}`,
            brand: getRandomElement(brands),
            type: getRandomElement(shoeTypes),
            material: getRandomElement(materials),
            colors: [getRandomElement(colors), getRandomElement(colors)], // Nhiều màu sắc
            soles: [getRandomElement(soles)], // Đế
            sizes: [getRandomElement(sizes), getRandomElement(sizes)], // Nhiều kích thước
            price: generateRandomPrice(500000, 5000000),
            imageUrl: `https://via.placeholder.com/150/000000/FFFFFF/?text=Product${i + 1}`,
            originalPrice: originalPrice, // Giá gốc
            salePrice: salePrice, // Giá khuyến mãi có thể có
            sale: salePercentage // Tính phần trăm giảm giá
        });
    }
}

generateFakeProducts();

// Display Products
let currentPage = 1;
let rowsPerPage = 5;

function displayProducts(productsList, page = 1, rows = rowsPerPage) {
    const productContainer = document.getElementById('product-list');
    productContainer.innerHTML = '';
    page--;

    const start = page * rows;
    const end = start + rows;
    const paginatedItems = productsList.slice(start, end);

    paginatedItems.forEach(product => {
        const colorOptions = product.colors.map(color => 
            `<div class="color-circle bg-${color} rounded-circle me-2" style="width: 20px; height: 20px;" data-color="${color}"></div>`
        ).join('');
        
        const sizeOptions = product.sizes.map(size => 
            `<div class="size-circle rounded-circle me-2" data-size="${size}">${size}</div>`
        ).join('');
        
        const productCard = `
            <div class="col-md-6 col-lg-4 col-xl-3 mb-4 product-card">
                <div class="shadow-sm border-0">
                    <div class="product-image position-relative">
                        <img src="${product.imageUrl}" class="img-fluid w-100 rounded-top" alt="${product.name}" style="transition: transform 0.3s ease;">
                        ${product.sale ? `<div class="discount-badge position-absolute bg-danger text-white rounded-pill px-2" style="top: 15px; left: 15px; font-size: 0.85rem;">-${product.sale}%</div>` : ''}
                    </div>
                    
                    <div class="product-content p-3 bg-white rounded-bottom">
                        <div class="mb-2">
                            <div class="d-flex mt-1">
                                ${colorOptions}
                            </div>
                        </div>
                        
                        <h5 class="mb-2 text-primary">${product.name}</h5>
                        <p class="text-muted mb-2">${product.type}</p>
                        
                        <div class="size-options mb-2">
                            <div class="d-flex mt-1">
                                ${sizeOptions}
                            </div>
                        </div>
                        
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                ${product.salePrice ? 
                                    `<p class="fs-5 fw-bold mb-0 original-price" style="text-decoration: line-through; color: gray;">${parseFloat(product.originalPrice).toLocaleString('en-US', { style: 'currency', currency: 'USD' })}</p>
                                     <p class="fs-5 fw-bold mb-0 sale-price">${parseFloat(product.salePrice).toLocaleString('en-US', { style: 'currency', currency: 'USD' })}</p>` 
                                    : 
                                    `<p class="fs-5 fw-bold mb-0 original-price">${parseFloat(product.originalPrice).toLocaleString('en-US', { style: 'currency', currency: 'USD' })}</p>`}
                            </div>
                            <div class="action-icons">
                                <a href="#" class="text-primary me-2 add-to-cart">
                                    <i class="fa fa-shopping-cart"></i>
                                </a>
                                <a href="#" class="text-secondary view-details">
                                    <i class="fa fa-info-circle"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`;
            
        productContainer.innerHTML += productCard;
    });
    setupPagination(productsList.length, rows, page + 1);
}

// Giả sử bạn có một phần tử chứa sản phẩm
document.addEventListener('DOMContentLoaded', () => {
    displayProducts(products);
});

// Pagination
function setupPagination(totalItems, rowsPerPage, currentPage) {
    const paginationContainer = document.getElementById('pagination');
    paginationContainer.innerHTML = '';

    const pageCount = Math.ceil(totalItems / rowsPerPage);
    const maxPagesToShow = 5; // Số trang tối đa để hiển thị
    let startPage, endPage;

    if (pageCount <= maxPagesToShow) {
        // Nếu tổng số trang ít hơn hoặc bằng số trang tối đa
        startPage = 1;
        endPage = pageCount;
    } else {
        // Tính toán startPage và endPage
        if (currentPage <= Math.floor(maxPagesToShow / 2)) {
            startPage = 1;
            endPage = maxPagesToShow;
        } else if (currentPage + Math.floor(maxPagesToShow / 2) >= pageCount) {
            startPage = pageCount - maxPagesToShow + 1;
            endPage = pageCount;
        } else {
            startPage = currentPage - Math.floor(maxPagesToShow / 2);
            endPage = currentPage + Math.floor(maxPagesToShow / 2);
        }
    }

    for (let i = startPage; i <= endPage; i++) {
        const pageItem = document.createElement('li');
        pageItem.classList.add('page-item');
        if (i === currentPage) {
            pageItem.classList.add('active');
        }
        pageItem.innerHTML = `<a class="page-link" href="#">${i}</a>`;
        pageItem.addEventListener('click', function () {
            currentPage = i;
            displayProducts(products, currentPage, rowsPerPage);
        });
        paginationContainer.appendChild(pageItem);
    }
}

// Filters & Search
function filterProducts() {
    const shoeType = document.getElementById('shoeType').value;
    const brand = document.getElementById('brand').value;
    const material = document.getElementById('material').value;
    const sole = document.getElementById('sole').value;
    const size = document.getElementById('size').value;
    const color = document.getElementById('color').value;
    const minPrice = parseInt(document.getElementById('minPrice').value) || 0;
    const searchQuery = document.getElementById('searchInput').value.toLowerCase();

    let filteredProducts = products.filter(product => {
        return (
            (shoeType === '' || product.type === shoeType) &&
            (brand === '' || product.brand === brand) &&
            (material === '' || product.material === material) &&
            (sole === '' || product.sole === sole) &&
            (size === '' || product.size == size) &&
            (color === '' || product.color === color) &&
            (product.price >= minPrice) &&
            (product.name.toLowerCase().includes(searchQuery))
        );
    });
    displayProducts(filteredProducts, currentPage, rowsPerPage);
}

// Event Listeners
document.getElementById('shoeType').addEventListener('change', filterProducts);
document.getElementById('brand').addEventListener('change', filterProducts);
document.getElementById('material').addEventListener('change', filterProducts);
document.getElementById('sole').addEventListener('change', filterProducts);
document.getElementById('size').addEventListener('change', filterProducts);
document.getElementById('color').addEventListener('change', filterProducts);
document.getElementById('minPrice').addEventListener('input', function() {
    document.getElementById('priceDisplay').innerText = `${this.value} VND - 5,000,000 VND`;
    filterProducts();
});
document.getElementById('searchButton').addEventListener('click', filterProducts);
document.getElementById('resetFilters').addEventListener('click', function () {
    document.getElementById('shoeType').value = '';
    document.getElementById('brand').value = '';
    document.getElementById('material').value = '';
    document.getElementById('sole').value = '';
    document.getElementById('size').value = '';
    document.getElementById('color').value = '';
    document.getElementById('minPrice').value = 0;
    document.getElementById('searchInput').value = '';
    document.getElementById('priceDisplay').innerText = `0 VND - 5,000,000 VND`;
    filterProducts();
});
document.getElementById('rowsPerPage').addEventListener('change', function () {
    rowsPerPage = parseInt(this.value);
    displayProducts(products, currentPage, rowsPerPage);
});

// Initial Display
displayProducts(products, currentPage, rowsPerPage);
