const pauseIcon = document.getElementById("pauseIcon");
pauseIcon.classList.add("hidden");

function toggleVideo(button) {
    const video = button.querySelector("video");
    const playIcon = document.getElementById("playIcon");

    if (video.paused) {
        video.play();
        playIcon.classList.add("hidden"); // Thêm lớp 'hidden' để ẩn biểu tượng Play
        pauseIcon.classList.remove("hidden"); // Loại bỏ lớp 'hidden' để hiện biểu tượng Pause
    } else {
        video.pause();
        playIcon.classList.remove("hidden"); // Loại bỏ lớp 'hidden' để hiện biểu tượng Play
        pauseIcon.classList.add("hidden"); // Thêm lớp 'hidden' để ẩn biểu tượng Pause
    }
}

// Thêm sự kiện hover cho nút
const detailVideoButton = document.querySelector(
    ".detail-proposal-video"
);
detailVideoButton.addEventListener("mouseover", function () {
    const video = this.querySelector("video");
    if (!video.paused) {
        // Nếu video đang chạy, hiện biểu tượng Pause
        pauseIcon.classList.remove("hidden");
    }
});
detailVideoButton.addEventListener("mouseout", function () {
    const video = this.querySelector("video");
    if (!video.paused) {
        // Nếu video đang chạy, ẩn biểu tượng Pause
        pauseIcon.classList.add("hidden");
    }
});