
$(function () {

    // Bar chart for dashboard
    $.ajax({
        url: '/Dashboard/EmployeesWithTheMostAssets',
        type: 'GET',
        success: function (response) {

            let labels = response.map(item => item.employeeName);
            let dataPoints = response.map(item => item.assetCount);

            let ctx = document.getElementById('barChart');

            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Pracownicy z największą ilością zasobów',
                        data: dataPoints,
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            })
        }
    })
});