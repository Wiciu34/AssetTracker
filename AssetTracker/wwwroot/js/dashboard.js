
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
                        label: 'liczba zasobów',
                        data: dataPoints,
                        borderWidth: 1
                    }]
                },
                options: {
                    plugins: {
                        title: {
                            display: true,
                            text: "Pracownicy z największą ilością zasobów",
                            padding: {
                                bottom: 20
                            }
                        }
                    },
                    layout: {
                        padding: {
                            top: 20,
                            bottom: 20,
                            left: 20,
                            right: 20
                        },
                    },
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
                type: 'doughnut',
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
                    layout: {
                        padding: {
                            top: 10, // Odstęp od góry (np. od legendy)
                            bottom: 10,
                            left: 10,
                            right: 10
                        },
                    },
                    plugins: {
                        title: {
                            display: true,
                            text: "Rozkład zasobów",
                            padding: {
                                bottom: 30
                            }
                        },
                        legend: {
                            position: 'top',
                            labels: {
                                padding: 10
                            }
                        }
                    },

                   

                }
            })

        },
        error: function (error) {
            console.error("Błąd podczas pobiernaia danych:", error);
        }
    });
});