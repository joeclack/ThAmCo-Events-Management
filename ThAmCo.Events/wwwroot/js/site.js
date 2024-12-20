document.addEventListener('DOMContentLoaded', function () {
    // Create Event Modal
    const createModal = document.getElementById('createEventModal');
    const openCreateModal = document.getElementById('openCreateModal');
    const closeCreateModal = document.getElementById('closeCreateModal');
    const cancelCreateModal = document.getElementById('cancelCreateModal');

    openCreateModal.addEventListener('click', () => createModal.classList.add('show'));
    closeCreateModal.addEventListener('click', () => createModal.classList.remove('show'));
    cancelCreateModal.addEventListener('click', () => createModal.classList.remove('show'));

});

document.addEventListener('DOMContentLoaded', function () {

    const bookMenuModal = document.getElementById('bookMenuModal');
    const openBookMenuModal = document.getElementById('openBookMenuModal');
    const closeBookMenuModal = document.getElementById('closeBookMenuModal');
    const cancelBookMenuModal = document.getElementById('cancelBookMenuModal');

    openBookMenuModal.addEventListener('click', () => bookMenuModal.classList.add('show'));
    closeBookMenuModal.addEventListener('click', () => bookMenuModal.classList.remove('show'));
    cancelBookMenuModal.addEventListener('click', () => bookMenuModal.classList.remove('show'));
});

document.addEventListener('DOMContentLoaded', function () {

    const createReservationModal = document.getElementById('createReservationModal');
    const openCreateReservationModal = document.getElementById('openCreateReservationModal');
    const closeCreateReservationModal = document.getElementById('closeCreateReservationModal');
    const cancelCreateReservationModal = document.getElementById('cancelCreateReservationModal');

    openCreateReservationModal.addEventListener('click', () => createReservationModal.classList.add('show'));
    closeCreateReservationModal.addEventListener('click', () => createReservationModal.classList.remove('show'));
    cancelCreateReservationModal.addEventListener('click', () => createReservationModal.classList.remove('show'));
}); 

document.addEventListener('DOMContentLoaded', function () {
    // Mobile Navigation
    const openMobileNavButton = document.getElementById('openMobileNavButton');
    const closeMobileNavButton = document.getElementById('closeMobileNavButton');
    const navLinks = document.getElementById('nav-links');

    function toggleMobileNav() {
        navLinks.classList.toggle('active');
        openMobileNavButton.style.display = navLinks.classList.contains('active') ? 'none' : 'inline-block';
        closeMobileNavButton.style.display = navLinks.classList.contains('active') ? 'inline-block' : 'none';
    }

    openMobileNavButton.addEventListener('click', toggleMobileNav);
    closeMobileNavButton.addEventListener('click', toggleMobileNav);

    // Handle dropdown on mobile
    const cateringDropdown = document.getElementById('cateringDropdown');
    const dropdownMenuLinks = document.querySelector('.dropdown-menu-links');

    if (window.innerWidth <= 768) {
        cateringDropdown.addEventListener('click', function (e) {
            e.preventDefault();
            dropdownMenuLinks.style.display =
                dropdownMenuLinks.style.display === 'block' ? 'none' : 'block';
        });
    }

    // Close mobile menu when clicking outside
    document.addEventListener('click', function (e) {
        if (!e.target.closest('.navbar') && navLinks.classList.contains('active')) {
            toggleMobileNav();
        }
    });

    // Handle window resize
    window.addEventListener('resize', function () {
        if (window.innerWidth > 768) {
            navLinks.classList.remove('active');
            openMobileNavButton.style.display = 'none';
            closeMobileNavButton.style.display = 'none';
            dropdownMenuLinks.style.display = '';
        } else {
            openMobileNavButton.style.display = 'inline-block';
        }
    });


});