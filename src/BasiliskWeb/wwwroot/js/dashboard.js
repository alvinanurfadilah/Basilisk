class Dashboard{
    constructor(annualIncomeChart, salesmenComparisonChart, salesmanPerformanceChart, categoryPopularityChart, incomeByRegionChart, customerActivityChart, customerInterestChart){
        this.annualIncomeChart = annualIncomeChart;
        this.salesmenComparisonChart = salesmenComparisonChart;
        this.salesmanPerformanceChart = salesmanPerformanceChart;
        this.categoryPopularityChart = categoryPopularityChart;
        this.incomeByRegionChart = incomeByRegionChart;
        this.customerActivityChart = customerActivityChart;
        this.customerInterestChart = customerInterestChart;
    }
}

(async function(){
    let dashboard = new Dashboard();

    dashboard.annualIncomeChart = await startAnnualIncome();
    refreshAnnualIncome(dashboard);

    dashboard.salesmenComparisonChart = await startSalesmenComparison();
    refreshSalesmenComparison(dashboard);

    dashboard.salesmanPerformanceChart = await startSalesmanPerformance();
    refreshSalesmanPerformance(dashboard);

    dashboard.incomeByRegionChart = await startIncomeByRegion();
    refreshIncomeByRegion(dashboard);

    dashboard.customerActivityChart = await startCustomerActivity();
    refreshCustomerActivity(dashboard);

    dashboard.categoryPopularityChart = await startCategoryPopularity();
    refreshCategoryPopularity(dashboard);

    dashboard.customerInterestChart = await startCustomerInterest();
    refreshCustomerInterest(dashboard);
}());

async function requestDto(url, parameters){
    let promise = await fetch(`${url}${parameters}`);
    let response = await promise.json();
    return response;
}

function renderChart(element, options){
    let chart = new ApexCharts(element, options);
    chart.render();
    return chart;
}

function formattingNumber(number) {
    let formattedNumber = Number(number).toFixed(2).toString();
    let [integerPart, decimalPart] = formattedNumber.split(".");
    let integerWithCommas = integerPart.replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    return decimalPart ? integerWithCommas + "," + decimalPart : integerWithCommas;
}

/*Annual Income*/

async function startAnnualIncome (){
    let year = document.querySelector(".annual-income .order-year").value;
    let dto = await requestDto('http://localhost:7070/dashboard/annualIncome/', year);
    let options = getAnnualIncomeChartOptions(dto);
    let element = document.querySelector('.annual-income .chart');
    let chart = renderChart(element, options);
    renderAnnualIncomeAnalysis(dto);
    return chart;
}

function getAnnualIncomeChartOptions(dto){
    let options = {
        colors: ['#14acf8'],
        series: [{
          name: 'Incomes',
          data: dto.incomes
        }],
        chart: {
            foreColor: '#9badd5',
            type: 'area',
            height: 450,
            zoom: {
                enabled: false
            }
        },
        dataLabels: {
          enabled: false
        },
        stroke: {
          curve: 'smooth'
        },
        labels: ["January","February","March","April","May","June","July","August","September","October","November","December"],
        yaxis: {
          opposite: true,
          labels: {
            formatter: formattingNumber
          }
        },
    };
    return options;
}

function renderAnnualIncomeAnalysis({year, totalIncome, fluctuation, highestPeriod, lowestPeriod}){
    let yearElements = document.querySelectorAll('.annual-income .year');
    for(let element of yearElements){
        element.textContent = year;
    }
    let totalIncomeElement = document.querySelector(".annual-income .total-income");
    totalIncomeElement.textContent = formattingNumber(totalIncome);
    let fluctuationElement = document.querySelector(".annual-income .fluctuation");
    fluctuationElement.textContent = formattingNumber(fluctuation);
    let highestPeriodElement = document.querySelector(".annual-income .highest-period");
    highestPeriodElement.textContent = highestPeriod;
    let lowestPeriodElement = document.querySelector(".annual-income .lowest-period");
    lowestPeriodElement.textContent = lowestPeriod;
}

function refreshAnnualIncome({annualIncomeChart}){
    let yearDropdown = document.querySelector(".annual-income .order-year");
    yearDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let dto = await requestDto('http://localhost:7070/dashboard/annualIncome/', year);
        annualIncomeChart.updateSeries([{name: 'Incomes', data: dto.incomes}]);
        renderAnnualIncomeAnalysis(dto);
    });
}

/*Salesman Comparison*/

async function startSalesmenComparison(){
    let year = document.querySelector(".salesmen-comparison .order-year").value;
    let dto = await requestDto('http://localhost:7070/dashboard/salesmenComparison/', year);
    let options = getSalesmenComparisonChartOptions(dto);
    let element = document.querySelector('.salesmen-comparison .chart');
    return renderChart(element, options);
}

function getSalesmenComparisonChartOptions({salesmenNames, salesmenPerformances}){
    var options = {
        series: salesmenPerformances,
        chart: {
            foreColor: '#9badd5',
            width: 650,
            type: 'pie',
        },
        labels: salesmenNames,
    };
    return options;
}

function refreshSalesmenComparison({salesmenComparisonChart}){
    let yearDropdown = document.querySelector(".salesmen-comparison .order-year");
    yearDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let dto = await requestDto('http://localhost:7070/dashboard/salesmenComparison/', year);
        salesmenComparisonChart.updateSeries(dto.salesmenPerformances);
        salesmenComparisonChart.updateOptions({labels: dto.salesmenNames});
    });
}

/*Salesman Performance*/

async function startSalesmanPerformance(){
    let year = document.querySelector(".salesman-performance .order-year").value;
    let employeeNumber = document.querySelector(".salesman-performance .salesman-employee-number").value;
    let parameters = `?employeeNumber=${employeeNumber}&year=${year}`;
    let dto = await requestDto('http://localhost:7070/dashboard/salesmanPerformance', parameters);
    let options = getSalesmanPerformanceChartOptions(dto);
    let element = document.querySelector('.salesman-performance .chart');
    return renderChart(element, options);
}

function getSalesmanPerformanceChartOptions(dto){
    var options = {
        colors: ['#14acf8'],
        series: [{
          data: dto
        }],
        chart: {
          type: 'bar',
          height: 400,
          foreColor: '#9badd5',
        },
        plotOptions: {
          bar: {
            borderRadius: 4,
            borderRadiusApplication: 'end',
            horizontal: false,
          }
        },
        dataLabels: {
          enabled: false
        },
        xaxis: {
          categories: ["January","February","March","April","May","June","July","August","September","October","November","December"],
        }
    };
    return options;
}

function refreshSalesmanPerformance({salesmanPerformanceChart}){
    let yearDropdown = document.querySelector(".salesman-performance .order-year");
    let employeeNumberDropdown = document.querySelector(".salesman-performance .salesman-employee-number");

    yearDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let employeeNumber = employeeNumberDropdown.value;
        let parameters = `?employeeNumber=${employeeNumber}&year=${year}`;
        let dto = await requestDto('http://localhost:7070/dashboard/salesmanPerformance', parameters);
        salesmanPerformanceChart.updateSeries([{data: dto}]);
    });

    employeeNumberDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let employeeNumber = employeeNumberDropdown.value;
        let parameters = `?employeeNumber=${employeeNumber}&year=${year}`;
        let dto = await requestDto('http://localhost:7070/dashboard/salesmanPerformance', parameters);
        salesmanPerformanceChart.updateSeries([{data: dto}]);
    });
}

/*Income By Region*/

async function startIncomeByRegion(){
    let year = document.querySelector(".income-by-region .order-year").value;
    let dto = await requestDto('http://localhost:7070/dashboard/incomeByRegion/', year);
    let options = getIncomeByRegionChartOptions(dto);
    let element = document.querySelector('.income-by-region .chart');
    return renderChart(element, options);
}

function getIncomeByRegionChartOptions({cities, incomes}){
    var options = {
        series: incomes,
        chart: {
            foreColor: '#9badd5',
            width: 650,
            type: 'donut',
        },
        labels: cities,
    };
    return options;
}

function refreshIncomeByRegion({incomeByRegionChart}){
    let yearDropdown = document.querySelector(".income-by-region .order-year");
    yearDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let dto = await requestDto('http://localhost:7070/dashboard/incomeByRegion/', year);
        incomeByRegionChart.updateSeries(dto.incomes);
        incomeByRegionChart.updateOptions({labels: dto.cities});
    });
}

/*Customer Activity*/

async function startCustomerActivity(){
    let year = document.querySelector(".customer-activity .order-year").value;
    let customerId = document.querySelector(".customer-activity .customer-id").value;
    let parameters = `?customerId=${customerId}&year=${year}`;
    let dto = await requestDto('http://localhost:7070/dashboard/customerActivity', parameters);
    let options = getCustomerActivityChartOptions(dto);
    let element = document.querySelector('.customer-activity .chart');
    return renderChart(element, options);
}

function getCustomerActivityChartOptions(dto){
    var options = {
        colors: ['#14acf8'],
        series: [{
          data: dto
        }],
        chart: {
          type: 'bar',
          height: 400,
          foreColor: '#9badd5',
        },
        plotOptions: {
          bar: {
            borderRadius: 4,
            borderRadiusApplication: 'end',
            horizontal: false,
          }
        },
        dataLabels: {
          enabled: false
        },
        xaxis: {
          categories: ["January","February","March","April","May","June","July","August","September","October","November","December"],
        }
    };
    return options;
}

function refreshCustomerActivity({customerActivityChart}){
    let yearDropdown = document.querySelector(".customer-activity .order-year");
    let customerIdDropdown = document.querySelector(".customer-activity .customer-id");

    yearDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let customerId = customerIdDropdown.value;
        let parameters = `?customerId=${customerId}&year=${year}`;
        let dto = await requestDto('http://localhost:7070/dashboard/customerActivity', parameters);
        customerActivityChart.updateSeries([{data: dto}]);
    });

    customerIdDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let customerId = customerIdDropdown.value;
        let parameters = `?customerId=${customerId}&year=${year}`;
        let dto = await requestDto('http://localhost:7070/dashboard/customerActivity', parameters);
        customerActivityChart.updateSeries([{data: dto}]);
    });
}

/*Category Popularity*/

async function startCategoryPopularity(){
    let year = document.querySelector(".category-popularity .order-year").value;
    let dto = await requestDto('http://localhost:7070/dashboard/categoryPopularity/', year);
    let options = getCategoryPopularityChartOptions(dto);
    let element = document.querySelector('.category-popularity .chart');
    return renderChart(element, options);
}

function getCategoryPopularityChartOptions({categoryNames, totalQuantity}){
    var options = {
        series: totalQuantity,
        chart: {
            foreColor: '#9badd5',
            width: 650,
            type: 'pie',
        },
        labels: categoryNames,
    };
    return options;
}

function refreshCategoryPopularity({categoryPopularityChart}){
    let yearDropdown = document.querySelector(".category-popularity .order-year");
    yearDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let dto = await requestDto('http://localhost:7070/dashboard/categoryPopularity/', year);
        categoryPopularityChart.updateSeries(dto.totalQuantity);
        categoryPopularityChart.updateOptions({labels: dto.categoryNames});
    });
}

/*Customer Interest*/

async function startCustomerInterest(){
    let year = document.querySelector(".customer-interest .order-year").value;
    let customerId = document.querySelector(".customer-interest .customer-id").value;
    let parameters = `?customerId=${customerId}&year=${year}`;
    let dto = await requestDto('http://localhost:7070/dashboard/customerInterest', parameters);
    let options = getCustomerInterestChartOptions(dto);
    let element = document.querySelector('.customer-interest .chart');
    return renderChart(element, options);
}

function getCustomerInterestChartOptions({categoryNames, totalQuantity}){
    var options = {
        series: [{
          name: 'Total Quantity',
          data: totalQuantity,
        }],
        chart: {
            foreColor: '#9badd5',
            height: 430,
            type: 'radar',
        },
        dataLabels: {
          enabled: true
        },
        plotOptions: {
          radar: {
            size: 200,
            polygons: {
              strokeColors: '#9badd5',
              fill: {
                colors: ['#182037', '#202a45']
              }
            }
          }
        },
        colors: ['#14acf8'],
        markers: {
          size: 4,
          colors: ['#fff'],
          strokeColor: '#14acf8',
          strokeWidth: 2,
        },
        tooltip: {
          y: {
            formatter: function(val) {
              return val
            }
          }
        },
        xaxis: {
          categories: categoryNames
        },
        yaxis: {
          tickAmount: 7,
          labels: {
            formatter: function(val, i) {
              if (i % 2 === 0) {
                return val
              } else {
                return ''
              }
            }
          }
        }
    };
    return options;
}

function refreshCustomerInterest({customerInterestChart}){
    let yearDropdown = document.querySelector(".customer-interest .order-year");
    let customerIdDropdown = document.querySelector(".customer-interest .customer-id");

    yearDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let customerId = customerIdDropdown.value;
        let parameters = `?customerId=${customerId}&year=${year}`;
        let dto = await requestDto('http://localhost:7070/dashboard/customerInterest', parameters);
        customerInterestChart.updateSeries([{data: dto.totalQuantity}]);
        customerInterestChart.updateOptions({ xaxis: {categories: dto.categoryNames }});
    });

    customerIdDropdown.addEventListener('change', async function(event){
        let year = yearDropdown.value;
        let customerId = customerIdDropdown.value;
        let parameters = `?customerId=${customerId}&year=${year}`;
        let dto = await requestDto('http://localhost:7070/dashboard/customerInterest', parameters);
        customerInterestChart.updateSeries([{data: dto.totalQuantity}]);
        customerInterestChart.updateOptions({ xaxis: {categories: dto.categoryNames }});
    });
}