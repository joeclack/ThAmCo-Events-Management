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