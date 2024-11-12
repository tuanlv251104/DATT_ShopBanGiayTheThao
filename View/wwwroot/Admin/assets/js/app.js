document.querySelectorAll('.has-arrow').forEach(function(link) {
    link.addEventListener('click', function() {
      const isExpanded = this.getAttribute('aria-expanded') === 'true';
      this.setAttribute('aria-expanded', !isExpanded);
  
      const submenu = this.nextElementSibling;
      if (!isExpanded) {
        submenu.classList.add('show');
      } else {
        submenu.classList.remove('show');
      }
    });
  });

// Tự động thêm số thứ tự cho mỗi dòng trong bảng
window.onload = function() {
  var rows = document.querySelectorAll("table tbody tr");
  rows.forEach((row, index) => {
      row.querySelector("td:first-child").textContent = index + 1; // Thêm STT vào ô đầu tiên
  });
};


const employees = [
  { id: 1, image: 'https://via.placeholder.com/40', code: 'NV001', name: 'Nguyễn Văn A', email: 'nguyenvana@example.com', phone: '0901234567', dob: '1990-01-15', gender: 'Nam', position: 'Kế toán', status: 'Hoạt động' },
  { id: 2, image: 'https://via.placeholder.com/40', code: 'NV002', name: 'Trần Thị B', email: 'tranthib@example.com', phone: '0912345678', dob: '1985-06-20', gender: 'Nữ', position: 'Quản lý', status: 'Ngừng hoạt động' },
  // Thêm dữ liệu khác tại đây...
];

const rowsPerPageSelect = document.getElementById('rowsPerPage');
const paginationControls = document.getElementById('paginationControls');
const employeeTable = document.getElementById('employeeTable');
let currentPage = 1;
let rowsPerPage = parseInt(rowsPerPageSelect.value);

// Hàm render bảng dữ liệu
function renderTable(data) {
  employeeTable.innerHTML = '';
  data.forEach((employee, index) => {
      const row = `<tr>
                      <td>${index + 1}</td>
                      <td><img src="${employee.image}" alt="Avatar" class="rounded-circle" width="40"></td>
                      <td>${employee.code}</td>
                      <td>${employee.name}</td>
                      <td>${employee.email}</td>
                      <td>${employee.phone}</td>
                      <td>${employee.dob}</td>
                      <td>${employee.gender}</td>
                      <td>${employee.position}</td>
                      <td><span class="badge bg-${employee.status === 'Hoạt động' ? 'success' : 'danger'}">${employee.status}</span></td>
                      <td><button class="btn btn-link p-0"><i class="ti ti-pencil text-info"></i></button></td>
                  </tr>`;
      employeeTable.innerHTML += row;
  });
}

// Hàm phân trang
function paginate(employees, page = 1, rows = 10) {
  const start = (page - 1) * rows;
  const end = start + rows;
  const paginatedData = employees.slice(start, end);
  renderTable(paginatedData);
  renderPaginationControls(employees.length, rows, page);
}

// Hàm render nút phân trang
function renderPaginationControls(totalItems, rowsPerPage, currentPage) {
  paginationControls.innerHTML = '';
  const totalPages = Math.ceil(totalItems / rowsPerPage);

  for (let i = 1; i <= totalPages; i++) {
      paginationControls.innerHTML += `<li class="page-item ${i === currentPage ? 'active' : ''}">
                                          <a class="page-link" href="#">${i}</a>
                                      </li>`;
  }

  // Bắt sự kiện khi nhấn vào nút phân trang
  document.querySelectorAll('.page-link').forEach(link => {
      link.addEventListener('click', (e) => {
          e.preventDefault();
          currentPage = parseInt(e.target.textContent);
          paginate(filteredEmployees, currentPage, rowsPerPage);
      });
  });
}

// Lọc dữ liệu theo các bộ lọc
function filterData() {
  const genderFilter = document.getElementById('filterGender').value;
  const positionFilter = document.getElementById('filterPosition').value;
  const statusFilter = document.getElementById('filterStatus').value;
  const searchQuery = document.getElementById('searchInput').value.toLowerCase();

  return employees.filter(employee => {
      const matchesGender = genderFilter ? employee.gender === genderFilter : true;
      const matchesPosition = positionFilter ? employee.position === positionFilter : true;
      const matchesStatus = statusFilter ? employee.status === statusFilter : true;
      const matchesSearch = employee.name.toLowerCase().includes(searchQuery) ||
                            employee.email.toLowerCase().includes(searchQuery) ||
                            employee.phone.includes(searchQuery);

      return matchesGender && matchesPosition && matchesStatus && matchesSearch;
  });
}

// Sự kiện thay đổi số dòng hiển thị mỗi trang
rowsPerPageSelect.addEventListener('change', (e) => {
  rowsPerPage = parseInt(e.target.value);
  paginate(filteredEmployees, currentPage, rowsPerPage);
});

// Sự kiện tìm kiếm và bộ lọc
document.getElementById('searchButton').addEventListener('click', () => {
  filteredEmployees = filterData();
  currentPage = 1;
  paginate(filteredEmployees, currentPage, rowsPerPage);
});

document.querySelectorAll('select').forEach(select => {
  select.addEventListener('change', () => {
      filteredEmployees = filterData();
      currentPage = 1;
      paginate(filteredEmployees, currentPage, rowsPerPage);
  });
});

// Khởi tạo trang
let filteredEmployees = employees;
paginate(filteredEmployees, currentPage, rowsPerPage);

// Function to preview the selected profile image
function previewImage() {
  const file = document.getElementById('profilePicture').files[0];
  const reader = new FileReader();
  reader.onload = function(e) {
      document.getElementById('profilePreview').src = e.target.result;
  }
  if (file) {
      reader.readAsDataURL(file);
  }
}

// Handling the QR Scan button (you can integrate real QR scanning functionality here)
document.getElementById('scanQrButton').addEventListener('click', function() {
  alert('Chức năng Quét QR chưa được tích hợp');
});


// Thêm sự kiện cho nút lưu sản phẩm
document.getElementById('saveProductBtn').addEventListener('click', function() {
  // Lấy thông tin từ form
  const productName = document.getElementById('productName').value;
  const category = document.getElementById('category').value;
  const brand = document.getElementById('brand').value;
  const sole = document.getElementById('sole').value;
  const material = document.getElementById('material').value;
  const productDescription = document.getElementById('productDescription').value;
  const color = document.getElementById('color').value;
  const size = document.getElementById('size').value;

  // Ghi lại thông tin sản phẩm (có thể thêm vào một danh sách hoặc gửi đến server)
  // addProductToList(productName, category, brand, sole, material, productDescription, color, size);

  // Đóng modal sau khi lưu
  var modal = bootstrap.Modal.getInstance(document.getElementById('addProductModal'));
  modal.hide();
  
  // Reset form
  document.getElementById('addProductForm').reset();
});


document.getElementById('colorSelect').addEventListener('change', function() {
  const variantList = document.getElementById('variantList');
  variantList.innerHTML = ''; // Clear previous variants

  const selectedColors = Array.from(this.selectedOptions).map(option => option.value);
  const selectedSizes = Array.from(document.getElementById('sizeSelect').selectedOptions).map(option => option.value);

  selectedColors.forEach(color => {
      const row = document.createElement('div');
      row.classList.add('mb-3', 'variant-row');
      row.innerHTML = `<strong>Màu: ${color}</strong>`;
      
      selectedSizes.forEach(size => {
          const sizeDiv = document.createElement('div');
          sizeDiv.classList.add('d-flex', 'align-items-center', 'mt-2');
          sizeDiv.innerHTML = `
              <span class="me-2">Kích cỡ: ${size}</span>
              <input type="number" placeholder="Số lượng" class="form-control w-25 me-2">
              <input type="number" placeholder="Giá" class="form-control w-25">
          `;
          row.appendChild(sizeDiv);
      });

      variantList.appendChild(row);
  });
});

document.getElementById('sizeSelect').addEventListener('change', function() {
  document.getElementById('colorSelect').dispatchEvent(new Event('change'));
});




