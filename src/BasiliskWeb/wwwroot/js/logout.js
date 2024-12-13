(() => {
    let logout = () => {
        let getButton = document.querySelector(".logout-button");
        getButton.addEventListener("click", (event) => {
            // event.preventDefault();
            localStorage.removeItem("token");
            localStorage.removeItem("regionId");
            localStorage.removeItem("employeeNumber");
            // location.reload();
        });
    };

    logout();
})();