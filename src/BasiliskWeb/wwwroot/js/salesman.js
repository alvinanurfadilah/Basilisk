(() => {
    const url = "http://localhost:5196/api/v1/salesman";
    let initialPage = 1;
    let initialEmployeeNumber = "";
    let initialFullName = "";
    let initialLevel = "";
    let initialSuperiorName = "";

    //#region DELETE SALESMAN
    let deleteSalesman = (employeeNumber) => {
        let request = new XMLHttpRequest();
        request.open("DELETE", `${url}/?employeeNumber=${employeeNumber}`);
        request.send();
        request.onload = () => {
            console.log(request.response);
        };
    };
    //#endregion

    //#region PUT SALESMAN
    let getSalesman = (employeeNumber) => {
        let request = new XMLHttpRequest();
        request.open(
            "GET",
            `${url}/${employeeNumber}`
        );
        request.send();
        request.onload = () => {
            let salesman = JSON.parse(request.response);
            letCommentEdit(salesman);
        };
    }

    let letCommentEdit = (salesman) => {
        console.log(salesman);
        let modal = document.querySelector(".modal-layer");
        modal.style.display = "flex";
        let getModalHeader = document.querySelector(".popup-dialog");
        getModalHeader.style.display = "block";
        let getButtonInsert = document.querySelector("button#insert");
        getButtonInsert.style.display = "none";

        let employeeNumber = document.querySelector("#employeeNumber");
        let firstName = document.querySelector("#firstName");
        let lastName = document.querySelector("#lastName");
        let level = document.querySelector("#level");
        let birthDate = document.querySelector("#birthDate");
        let hiredDate = document.querySelector("#hiredDate");
        let address = document.querySelector("#address");
        let city = document.querySelector("#city");
        let superiorEmployeeNumber = document.querySelector("#superior-dropdown");

        employeeNumber.value = salesman.employeeNumber;
        firstName.value = salesman.firstName;
        lastName.value = salesman.lastName;
        level.value = salesman.level;
        birthDate.value = salesman.birthDate;
        hiredDate.value = salesman.hiredDate;
        address.textContent = salesman.address;
        city.value = salesman.city;
        superiorEmployeeNumber.value = salesman.superiorEmployeeNumber;


        let getClose = document.querySelector(".popup-dialog header button");
        getClose.addEventListener("click", (event) => {
            // event.preventDefault();
            modal.style.display = "none";
            employeeNumber.value = "";
            firstName.value = "";
            lastName.value = "";
            level.value = "";
            birthDate.value = "";
            hiredDate.value = "";
            address.textContent = "";
            city.value = "";
            superiorEmployeeNumber.value = "";
        });
    }

    let getValueEdit = () => {
        let employeeNumber = document.querySelector("#employeeNumber");
        let firstName = document.querySelector("#firstName");
        let lastName = document.querySelector("#lastName");
        let level = document.querySelector("#level");
        let birthDate = document.querySelector("#birthDate");
        let hiredDate = document.querySelector("#hiredDate");
        let address = document.querySelector("#address");
        let city = document.querySelector("#city");
        let superiorEmployeeNumber = document.querySelector("#superior-dropdown");

        return {
            employeeNumber: employeeNumber.value,
            firstName: firstName.value,
            lastName: lastName.value,
            level: level.value,
            birthDate: birthDate.value,
            hiredDate: hiredDate.value,
            address: address.textContent,
            city: city.value,
            superiorEmployeeNumber: superiorEmployeeNumber.value,
        };
    }

    let editSalesman = () => {
        let request = new XMLHttpRequest();
        request.open("PUT", url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(getValueEdit()));
        request.onload = () => {
            console.log(request.response);
        };
    }

    let getButtonUpdate = () => {
        let getButtonUpdate = document.querySelector("button#update");
        getButtonUpdate.addEventListener("click", (event) => {
            // event.preventDefault();
            editSalesman();
        });
    }
    //#endregion

    //#region POST SALESMAN
    let letComment = () => {
        let modal = document.querySelector(".modal-layer");
        modal.style.display = "flex";
        let getModalHeader = document.querySelector(".popup-dialog");
        getModalHeader.style.display = "block";
        let getButtonUpdate = document.querySelector("button#update");
        getButtonUpdate.style.display = "none";

        let employeeNumber = document.querySelector("#employeeNumber");
        let firstName = document.querySelector("#firstName");
        let lastName = document.querySelector("#lastName");
        let level = document.querySelector("#level");
        let birthDate = document.querySelector("#birthDate");
        let hiredDate = document.querySelector("#hiredDate");
        let address = document.querySelector("#address");
        let city = document.querySelector("#city");
        let superiorEmployeeNumber = document.querySelector("#superior-dropdown");

        let getClose = document.querySelector(".popup-dialog header button");
        getClose.addEventListener("click", (event) => {
            // event.preventDefault();
            modal.style.display = "none";
            employeeNumber.value = "";
            firstName.value = "";
            lastName.value = "";
            level.value = "";
            birthDate.value = "";
            hiredDate.value = "";
            address.value = "";
            city.value = "";
            superiorEmployeeNumber.value = "";
        });

        return {
            employeeNumber: employeeNumber.value,
            firstName: firstName.value,
            lastName: lastName.value,
            level: level.value,
            birthDate: birthDate.value,
            hiredDate: hiredDate.value,
            address: address.value,
            city: city.value,
            superiorEmployeeNumber: superiorEmployeeNumber.value,
        };
    };

    let sendSaleman = () => {
        let request = new XMLHttpRequest();
        request.open("POST", `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(letComment()));
        request.onload = () => {
            console.log(request.response);
        };
    };

    let getDropdown = () => {
        let request = new XMLHttpRequest();
        request.open("GET", `${url}/all`);
        request.setRequestHeader("Content-Type", "application/json");
        request.onload = () => {
            let superiors = JSON.parse(request.response);
            let selectSuperior = document.querySelector("#superior-dropdown");
            superiors.forEach((sup) => {
                let option = new Option(
                    (text = sup.fullName),
                    (value = sup.employeeNumber)
                );
                selectSuperior.options.add(option);
            });
        };
        request.send();
    };

    let buttonSubmit = () => {
        let getButtonInsert = document.querySelector("button#insert");
        getButtonInsert.addEventListener("click", (event) => {
            // event.preventDefault();
            sendSaleman();
        });
    };
    //#endregion

    //#region GET SALESMAN
    let bindingSalesman = (salesman) => {
        for (let index = 0; index < salesman.length; index++) {
            let tr = document.createElement("tr");
            let td = {
                action: document.createElement("td"),
                employeeNumber: document.createElement("td"),
                fullName: document.createElement("td"),
                level: document.createElement("td"),
                superior: document.createElement("td"),
            };
            let a = {
                edit: document.createElement("a"),
                delete: document.createElement("a"),
                region: document.createElement("a"),
            };

            a.edit.setAttribute("class", "blue-button update-button");
            a.delete.setAttribute("class", "blue-button delete-button");
            a.region.setAttribute("class", "blue-button salesman-button");

            a.edit.textContent = "Edit";
            a.delete.textContent = "Delete";
            a.region.textContent = "Region";

            td.action.append(a.edit);
            td.action.append(a.delete);
            td.action.append(a.region);

            td.employeeNumber.textContent = salesman[index].employeeNumber;
            td.fullName.textContent = salesman[index].fullName;
            td.level.textContent = salesman[index].level;
            td.superior.textContent = salesman[index].superior;

            tr.append(td.action);
            tr.append(td.employeeNumber);
            tr.append(td.fullName);
            tr.append(td.level);
            tr.append(td.superior);

            let tbody = document.querySelector("#salesman");
            tbody.append(tr);

            a.edit.addEventListener("click", (event) => {
                event.preventDefault();
                getSalesman(salesman[index].employeeNumber);
            });

            a.delete.addEventListener("click", () => {
                deleteSalesman(salesman[index].employeeNumber);
                location.reload();
            });

            a.region.addEventListener("click", (event) => {
                event.preventDefault();
                localStorage.setItem("employeeNumber", salesman[index].employeeNumber);
                window.location.href = `/Salesman/SalesmanRegion`;
            });
        }

        let getButtonInsert = document.querySelector("#myBtn");
        getButtonInsert.addEventListener("click", (event) => {
            event.preventDefault();
            letComment();
        });
    };

    let filter = () => {
        let getButton = document.querySelector(".blue-button");
        getButton.addEventListener("click", (event) => {
            event.preventDefault();
            let getEmployeeNumber = document.querySelector(
                ".inputEmployeeNumber"
            ).value;
            let getFullName = document.querySelector(".inputFullName").value;
            let getLevel = document.querySelector(".inputLevel").value;
            let getSuperiorName =
                document.querySelector(".inputSuperiorName").value;
            salesman(
                initialPage,
                getEmployeeNumber,
                getFullName,
                getLevel,
                getSuperiorName
            );
        });
    };

    let pagination = (pagination) => {
        let div = document.createElement("div");
        let span = document.createElement("span");
        let divPagination = document.querySelector(".pagination");
        divPagination.innerHTML = "";

        for (let index = 1; index <= pagination.totalPages; index++) {
            let a = document.createElement("a");
            a.setAttribute("class", "number");
            a.addEventListener("click", (event) => {
                event.preventDefault();
                salesman(
                    a.textContent,
                    initialEmployeeNumber,
                    initialFullName,
                    initialLevel,
                    initialSuperiorName
                );
            });
            span.append(a);
            a.textContent = `${index}`;
        }

        div.textContent = `page ${pagination.pageNumber} of ${pagination.totalPages}`;

        divPagination.append(div);
        divPagination.append(span);

        let tfoot = document.querySelector("table tfoot tr td");
        tfoot.append(divPagination);
    };

    let salesman = (
        pageNumber,
        employeeNumber,
        fullName,
        level,
        superiorName
    ) => {
        let request = new XMLHttpRequest();
        request.open(
            "GET",
            `${url}?pageNumber=${pageNumber}&employeeNumber=${employeeNumber}&fullName=${fullName}&level=${level}&superiorName=${superiorName}`
        );
        request.send();
        request.onload = () => {
            let salesman = JSON.parse(request.response);

            let getBody = document.querySelector("#salesman");
            getBody.innerHTML = "";
            bindingSalesman(salesman.salesman);
            pagination(salesman.pagination);
        };
    };
    //#endregion

    getButtonUpdate();  

    getDropdown();
    buttonSubmit();

    filter();
    salesman(
        initialPage,
        initialEmployeeNumber,
        initialFullName,
        initialLevel,
        initialSuperiorName
    );
})();
