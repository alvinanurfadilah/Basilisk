(() => {
    const url = 'http://localhost:8080/api/v1/account'

    let letComment = () => {
        let username = document.querySelector('#username').value;
        let password = document.querySelector('#password').value;
        let role = document.querySelector('.role-user').value;

        return {username, password, role};
    }
    
    let tokens = () => {
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(letComment()));
        request.onload = () => {
            console.log(request.response);

            localStorage.setItem('token', request.response);
        };
    }

    let login = () => {
        let getButton = document.querySelector('.login');
        getButton.addEventListener('click', (event) => {
            // event.preventDefault();
            tokens();
        });
    }

    login();
})();