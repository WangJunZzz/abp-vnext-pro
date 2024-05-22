document.querySelectorAll(".zoom").forEach((item) => {
  item.addEventListener("click", function () {
    this.classList.toggle("image-zoom-large");
  });
});
