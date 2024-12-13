(() => {
    const url = "http://localhost:8080/api/v1/region";
    let initialPage = 1;
    let initialEmployeeNumber = "";
    let initialFullName = "";
    let initialLevel = "";
    let initialSuperiorName = "";

    //#region DELETE SALESMAN IN REGION
    let deleteSalesmanRegion = (employeeNumber) => {
        let regionId = localStorage.getItem("regionId");
        let request = new XMLHttpRequest();
        request.open("DELETE", `${url}/regionsalesman/?id=${regionId}&employeeNumber=${employeeNumber}`);
        request.setRequestHeader(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`
        ); // tambahkan pada setiap CRUD nya
        request.send();
        request.onload = () => {
            console.log(request.response);
        };
    }
    //#endregion

    //#region POST SALESMAN IN REGION
        let letComment = () => {
            let modal = document.querySelector(".modal-layer");
            modal.style.display = "flex";
            let getModalHeader = document.querySelector(".popup-dialog");
            getModalHeader.style.display = "block";

            let getClose = document.querySelector(".popup-dialog button");
            getClose.addEventListener("click", (event) => {
                event.preventDefault();
                city.value = "";
                remark.value = "";
                modal.style.display = "none";
            });

            let regionId = localStorage.getItem("regionId");
            let salesman = [document.querySelector('.salesman-dropdown').value];

            return {regionId, salesman }
        }

        let sendSalesman = () => {
            let request = new XMLHttpRequest();
            request.open("POST", `${url}/regionsalesman`);
            request.setRequestHeader("Content-Type", "application/json");
            request.setRequestHeader(
                "Authorization",
                `Bearer ${localStorage.getItem("token")}`
            ); // tambahkan pada setiap CRUD nya
            request.send(JSON.stringify(letComment()));
            request.onload = () => {
                console.log(request.response);
            };
        }

        let getDropdown = () => {
            let request = new XMLHttpRequest();
            request.open('GET', 'http://localhost:8080/api/v1/salesman/all');
            request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
            request.onload = () => {
                let salesman = JSON.parse(request.response);
    
                let selectShipment = document.querySelector('.salesman-dropdown');
                salesman.forEach((sal) => {
                    let option = new Option(text = sal.fullName, value = sal.employeeNumber);
                    selectShipment.options.add(option);
                });
            }
            request.send();
        }

        let buttonSubmit = () => {
            let getButtonInsert = document.querySelector("button#insert");
            getButtonInsert.addEventListener("click", (event) => {
                // event.preventDefault();
                sendSalesman();
            })
        }
    //#endregion

    //#region GET REGIONSALESMAN
    let bindingRegionSalesman = (regionSalesman) => {
        let getTitleBreadCrumbs = document.querySelector(".bread-crumbs-title");
        getTitleBreadCrumbs.textContent = regionSalesman.city;
        let getCity = document.querySelector("span#city");
        getCity.textContent = regionSalesman.city;
        let getRemark = document.querySelector("span#remark");
        getRemark.textContent = regionSalesman.remark;

        for (
            let index = 0;
            index < regionSalesman.regionSalesman.salesman.length;
            index++
        ) {
            let tr = document.createElement("tr");

            let a = document.createElement("a");
            let deleteSalesman = document.createElement("td");
            let employeeNumber = document.createElement("td");
            let fullName = document.createElement("td");
            let level = document.createElement("td");
            let superior = document.createElement("td");

            a.setAttribute("class", "blue-button delete-button");
            a.textContent = "Delete";
            employeeNumber.textContent =
                regionSalesman.regionSalesman.salesman[index].employeeNumber;
            fullName.textContent =
                regionSalesman.regionSalesman.salesman[index].fullName;
            level.textContent =
                regionSalesman.regionSalesman.salesman[index].level;
            superior.textContent =
                regionSalesman.regionSalesman.salesman[index].superior;

            deleteSalesman.append(a);

            tr.append(deleteSalesman);
            tr.append(employeeNumber);
            tr.append(fullName);
            tr.append(level);
            tr.append(superior);

            let getBody = document.querySelector("tbody#tbody");
            getBody.append(tr);

            a.addEventListener("click", () => {
                deleteSalesmanRegion(
                    regionSalesman.regionSalesman.salesman[index].employeeNumber
                );
                location.reload();
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
            let getEmployeeNumber = document.querySelector("#employeeNumberFilter").value;
            let getFullName = document.querySelector("#fullNameFilter").value;
            let getLevel = document.querySelector(".filter > select").value;
            let getSuperiorName = document.querySelector("#superiorNameFilter").value;
            regionSalesman(initialPage, getEmployeeNumber, getFullName, getLevel, getSuperiorName);
        });
    }

    let pagination = (pagination) => {
        console.log(pagination);
        let div = document.createElement("div");
        let span = document.createElement("span");
        let divPagination = document.querySelector(".pagination");
        divPagination.innerHTML = "";

        for (let index = 1; index <= pagination.totalPages; index++) {
            let a = document.createElement("a");
            a.setAttribute("class", "number");
            a.addEventListener("click", (event) => {
                event.preventDefault();
                regions(a.textContent, initialCity);
            });
            span.append(a);
            a.textContent = `${index}`;
        }

        div.textContent = `page ${pagination.pagination.pageNumber} of ${pagination.pagination.totalPages}`;

        divPagination.append(div);
        divPagination.append(span);

        let tfoot = document.querySelector("table tfoot tr td");
        tfoot.append(divPagination);
    };

    let regionSalesman = (pageNumber) => {
        let regionId = localStorage.getItem("regionId");
        let request = new XMLHttpRequest();
        request.open(
            "GET",
            `${url}/regionsalesman/${regionId}?pageNumber=${pageNumber}`
        );
        request.setRequestHeader(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`
        );
        request.send();
        request.onload = () => {
            let regionSalesman = JSON.parse(request.response);

            let getBody = document.querySelector("#tbody");
            getBody.innerHTML = "";
            bindingRegionSalesman(regionSalesman);
            pagination(regionSalesman.regionSalesman);
        };
    };
    //#endregion
    
    getDropdown();
    buttonSubmit();
    filter();
    regionSalesman(initialPage, initialEmployeeNumber, initialFullName, initialLevel, initialSuperiorName);
})();