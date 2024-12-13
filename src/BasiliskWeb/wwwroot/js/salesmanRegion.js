(() => {
    const url = "http://localhost:5196/api/v1/salesman";

    //#region DELETE REGION IN SALESMAN
    let deleteRegionSalesman = (regionId) => {
        let employeeNumber = localStorage.getItem("employeeNumber");
        let request = new XMLHttpRequest();
        request.open(
            "DELETE",
            `${url}/salesmanregion?employeeNumber=${employeeNumber}&regionId=${regionId}`
        );
        request.send();
        request.onload = () => {
            console.log(request.response);
        };
    };
    //#endregion

    //#region POST SALESMANREGION
    let letComment = () => {
        let modal = document.querySelector(".modal-layer");
        modal.style.display = "flex";
        let getModalHeader = document.querySelector(".popup-dialog");
        getModalHeader.style.display = "block";

        let getClose = document.querySelector(".popup-dialog button");
        getClose.addEventListener("click", (event) => {
            event.preventDefault();
            modal.style.display = "none";
        });

        let employeeNumber = localStorage.getItem("employeeNumber");
        let region = [document.querySelector(".region-dropdown").value];

        return { employeeNumber, region };
    };

    let sendRegion = () => {
        let request = new XMLHttpRequest();
        request.open("POST", `${url}/salesmanregion`);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(letComment()));
        request.onload = () => {
            console.log(request.response);
        };
    };

    let getDropdown = () => {
        let request = new XMLHttpRequest();
        request.open("GET", `${url}/allRegion`);
        request.onload = () => {
            let region = JSON.parse(request.response);

            let selectRegion = document.querySelector(".region-dropdown");
            region.forEach((reg) => {
                let option = new Option((text = reg.city), (value = reg.id));
                selectRegion.options.add(option);
            });
        };
        request.send();
    };

    let buttonSubmit = () => {
        let getButtonInsert = document.querySelector("button#insert");
        getButtonInsert.addEventListener("click", (event) => {
            // event.preventDefault();
            sendRegion();
        });
    };
    //#endregion

    //#region GET SALESMANREGION
    let bindingSalesmanRegion = (salesmanRegion) => {
        let getEmployeeNumberBreadCrumbs = document.querySelector(
            ".bread-crumbs-fullName"
        );
        getEmployeeNumberBreadCrumbs.textContent = salesmanRegion.fullName;
        let getEmployeeNumber = document.querySelector("span#employeeNumber");
        getEmployeeNumber.textContent = salesmanRegion.employeeNumber;
        let getFullName = document.querySelector("span#fullName");
        getFullName.textContent = salesmanRegion.fullName;

        for (
            let index = 0;
            index < salesmanRegion.salesmanRegion.regions.length;
            index++
        ) {
            let tr = document.createElement("tr");

            let a = document.createElement("a");
            let deleteRegion = document.createElement("td");
            let city = document.createElement("td");
            let remark = document.createElement("td");

            a.setAttribute("class", "blue-button delete-button");
            a.textContent = "Delete";
            city.textContent =
                salesmanRegion.salesmanRegion.regions[index].city;
            remark.textContent =
                salesmanRegion.salesmanRegion.regions[index].remark;

            deleteRegion.append(a);

            tr.append(deleteRegion);
            tr.append(city);
            tr.append(remark);

            let getBody = document.querySelector("tbody#tbody");
            getBody.append(tr);

            a.addEventListener("click", () => {
                deleteRegionSalesman(
                    salesmanRegion.salesmanRegion.regions[index].id
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

    let salesmanRegion = () => {
        let employeeNumber = localStorage.getItem("employeeNumber");
        let request = new XMLHttpRequest();
        request.open("GET", `${url}/salesmanregion/${employeeNumber}`);
        request.send();
        request.onload = () => {
            let salesmanRegion = JSON.parse(request.response);

            let getBody = document.querySelector("#tbody");
            getBody.innerHTML = "";
            bindingSalesmanRegion(salesmanRegion);
        };
    };
    //#endregion

    getDropdown();
    buttonSubmit();

    salesmanRegion();
})();
