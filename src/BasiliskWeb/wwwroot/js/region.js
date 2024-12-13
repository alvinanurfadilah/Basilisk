(() => {
    const url = "http://localhost:8080/api/v1/region";
    // const urlSalesman = "http://localhost:8080/api/v1/salesman";
    let initialPage = 1;
    let initialCity = "";

    //#region DELETE SALESMAN REGION
    // let deleteSalesmanRegion = (employeeNumber) => {
    //     let request = new XMLHttpRequest();
    //     request.open(
    //         "DELETE",
    //         `${urlSalesman}/?employeeNumber=${employeeNumber}`
    //     );
    //     request.setRequestHeader(
    //         "Authorization",
    //         `Bearer ${localStorage.getItem("token")}`
    //     ); // tambahkan pada setiap CRUD nya
    //     request.send();
    //     request.onload = () => {
    //         console.log(request.response);
    //     };
    // };
    //#endregion

    //#region SALESMAN REGION
    // let bindingSalesmanRegion = (regionSalesman) => {
    //     let getModalSalesman = document.querySelector("#modalSalesman");
    //     getModalSalesman.style.display = "flex";

    //     let getCity = document.querySelector("span#city");
    //     getCity.textContent = regionSalesman.city;
    //     let getRemark = document.querySelector("span#remark");
    //     getRemark.textContent = regionSalesman.remark;

    //     for (
    //         let index = 0;
    //         index < regionSalesman.regionSalesman.salesman.length;
    //         index++
    //     ) {
    //         let tr = document.createElement("tr");

    //         let a = document.createElement("a");
    //         let deleteSalesman = document.createElement("td");
    //         let employeeNumber = document.createElement("td");
    //         let fullName = document.createElement("td");
    //         let level = document.createElement("td");
    //         let superior = document.createElement("td");

    //         a.setAttribute("class", "blue-button delete-button");
    //         a.textContent = "Delete";
    //         employeeNumber.textContent =
    //             regionSalesman.regionSalesman.salesman[index].employeeNumber;
    //         fullName.textContent =
    //             regionSalesman.regionSalesman.salesman[index].fullName;
    //         level.textContent =
    //             regionSalesman.regionSalesman.salesman[index].level;
    //         superior.textContent =
    //             regionSalesman.regionSalesman.salesman[index].superior;

    //         deleteSalesman.append(a);

    //         tr.append(deleteSalesman);
    //         tr.append(employeeNumber);
    //         tr.append(fullName);
    //         tr.append(level);
    //         tr.append(superior);

    //         let getBody = document.querySelector("tbody#tbody");
    //         getBody.append(tr);

    //         a.addEventListener("click", () => {
    //             console.log(
    //                 regionSalesman.regionSalesman.salesman[index].employeeNumber
    //             );
    //             deleteSalesmanRegion(
    //                 regionSalesman.regionSalesman.salesman[index].employeeNumber
    //             );
    //             // location.reload();
    //         });
    //     }
    // };

    // let salesmanRegion = (id) => {
    //     let request = new XMLHttpRequest();
    //     request.open("GET", `${url}/regionsalesman?id=${id}`);
    //     request.setRequestHeader(
    //         "Authorization",
    //         `Bearer ${localStorage.getItem("token")}`
    //     ); // tambahkan pada setiap CRUD nya
    //     request.send();
    //     request.onload = () => {
    //         let regionSalesman = JSON.parse(request.response);
    //         console.log(regionSalesman);
    //         bindingSalesmanRegion(regionSalesman);
    //     };
    // };
    //#endregion

    //#region DELETE REGION
    let deleteRegion = (id) => {
        let request = new XMLHttpRequest();
        request.open("DELETE", `${url}/?id=${id}`);
        request.setRequestHeader(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`
        ); // tambahkan pada setiap CRUD nya
        request.send();
        request.onload = () => {
            console.log(request.response);
        };
    };

    //#endregion

    //#region PUT REGION
    let letCommentEdit = (region) => {
        let modal = document.querySelector(".modal-layer");
        modal.style.display = "flex";
        let getModalHeader = document.querySelector(".popup-dialog");
        getModalHeader.style.display = "block";
        let getButtonInsert = document.querySelector("button#insert");
        getButtonInsert.style.display = "none";

        let regionId = document.querySelector(
            "form table tbody tr:nth-child(1) td:nth-child(1) label#regionId"
        );
        regionId.value = region.id;

        let city = document.querySelector(
            "form table tbody tr:nth-child(1) td:nth-child(2) input"
        );
        city.value = region.city;
        let remark = document.querySelector(
            "form table tbody tr:nth-child(2) td:nth-child(2) textarea"
        );
        remark.value = region.remark;

        let getClose = document.querySelector(".popup-dialog button");
        getClose.addEventListener("click", (event) => {
            event.preventDefault();
            city.value = "";
            remark.value = "";
            modal.style.display = "none";
        });
    };

    let getValueEdit = () => {
        let regionId = document.querySelector(
            "form table tbody tr:nth-child(1) td:nth-child(1) label#regionId"
        );

        let city = document.querySelector(
            "form table tbody tr:nth-child(1) td:nth-child(2) input"
        );
        let remark = document.querySelector(
            "form table tbody tr:nth-child(2) td:nth-child(2) textarea"
        );

        return { id: regionId.value, city: city.value, remark: remark.value };
    };

    let editRegion = () => {
        let request = new XMLHttpRequest();
        request.open("PUT", `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.setRequestHeader(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`
        ); // tambahkan pada setiap CRUD nya
        request.send(JSON.stringify(getValueEdit()));
        request.onload = () => {
            console.log(request.response);
        };
    };

    let getButtonUpdate = () => {
        let getButtonUpdate = document.querySelector("button#update");
        getButtonUpdate.addEventListener("click", (event) => {
            // event.preventDefault();
            editRegion();
        });
    };
    //#endregion

    //#region POST REGION
    let letComment = () => {
        let modal = document.querySelector(".modal-layer");
        modal.style.display = "flex";
        let getModalHeader = document.querySelector(".popup-dialog");
        getModalHeader.style.display = "block";
        let getButtonUpdate = document.querySelector("button#update");
        getButtonUpdate.style.display = "none";

        let city = document.querySelector(
            "form table tbody tr:nth-child(1) td:nth-child(2) input"
        );
        let remark = document.querySelector(
            "form table tbody tr:nth-child(2) td:nth-child(2) textarea"
        );

        let getClose = document.querySelector(".popup-dialog button");
        getClose.addEventListener("click", (event) => {
            event.preventDefault();
            city.value = "";
            remark.value = "";
            modal.style.display = "none";
        });

        return { id: 1, city: city.value, remark: remark.value };
    };

    let sendRegion = () => {
        let request = new XMLHttpRequest();
        request.open("POST", `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.setRequestHeader(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`
        ); // tambahkan pada setiap CRUD nya
        request.send(JSON.stringify(letComment()));
        request.onload = () => {
            console.log(request.response);
        };
    };

    let buttonSubmit = () => {
        let getButtonInsert = document.querySelector("button#insert");
        getButtonInsert.addEventListener("click", (event) => {
            // event.preventDefault();
            sendRegion();
        });
    };
    //#endregion

    //#region GET REGION
    let bindingRegions = (regions) => {
        for (let index = 0; index < regions.length; index++) {
            let tr = document.createElement("tr");
            let td = {
                action: document.createElement("td"),
                city: document.createElement("td"),
                remark: document.createElement("td"),
            };
            let a = {
                edit: document.createElement("a"),
                delete: document.createElement("a"),
                salesman: document.createElement("a"),
            };

            a.edit.setAttribute("class", "blue-button update-button");
            a.delete.setAttribute("class", "blue-button delete-button");
            a.salesman.setAttribute("class", "blue-button salesman-button");

            a.edit.textContent = "Edit";
            a.delete.textContent = "Delete";
            a.salesman.textContent = "Salesman";

            td.action.append(a.edit);
            td.action.append(a.delete);
            td.action.append(a.salesman);

            td.city.textContent = regions[index].city;
            td.remark.textContent = regions[index].remark;

            tr.append(td.action);
            tr.append(td.city);
            tr.append(td.remark);

            let tbody = document.querySelector(".region");
            tbody.append(tr);

            a.edit.addEventListener("click", (event) => {
                event.preventDefault();
                letCommentEdit(regions[index]);
            });

            a.delete.addEventListener("click", () => {
                deleteRegion(regions[index].id);
                location.reload();
            });

            a.salesman.addEventListener("click", (event) => {
                event.preventDefault();
                localStorage.setItem("regionId", regions[index].id);
                window.location.href = `/Region/RegionSalesman`;
                // salesmanRegion(regions[index].id);
            });
        }

        let getButtonInsert = document.querySelector("#myBtn");
        getButtonInsert.addEventListener("click", (event) => {
            event.preventDefault();
            letComment();
        });
    };

    let city = () => {
        let getButton = document.querySelector(".blue-button");
        getButton.addEventListener("click", (event) => {
            event.preventDefault();
            let getInput = document.querySelector(".filter > input").value;
            regions(initialPage, getInput);
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
                regions(a.textContent, initialCity);
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

    let regions = (pageNumber, searchCity) => {
        let request = new XMLHttpRequest();
        request.open(
            "GET",
            `${url}?pageNumber=${pageNumber}&city=${searchCity}`
        );
        request.setRequestHeader(
            "Authorization",
            `Bearer ${localStorage.getItem("token")}`
        ); // tambahkan pada setiap CRUD nya
        request.send();
        request.onload = () => {
            let regions = JSON.parse(request.response);

            let getBody = document.querySelector(".region");
            getBody.innerHTML = "";
            bindingRegions(regions.regions);
            pagination(regions.pagination);
        };
    };
    //#endregion

    getButtonUpdate();
    buttonSubmit();
    city();
    regions(initialPage, initialCity);


    regionSalesman();
})();
