
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
                    responsive: true,
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            })
        },
        error: function (error) {
            console.error("Błąd podczas pobiernaia danych:", error);
        }
    });

    // Pie chart for dasboard

    $.ajax({
        url: '/Dashboard/AssetsForPieChart',
        type: 'GET',
        success: function (response) {

            let ctx = document.getElementById('pieChart');

            new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['Przypisane zasoby', 'Nieprzypisane zasoby'],
                    datasets: [{
                        label: 'Zasoby',
                        data: [response.assigned, response.unassigned],
                        backgroundColor: [
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(255, 99, 132, 0.2)'
                        ],
                        borderColor: [
                            'rgba(75, 192, 192, 1)',
                            'rgba(255, 99, 132, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                        }
                    }
                }
            })

        },
        error: function (error) {
            console.error("Błąd podczas pobiernaia danych:", error);
        }
    });
});