(function ($) {
    $(document).ready(function () {
        var projectGroupId = $("#statisticsProjectGroupId").val();
        var projectId = $("#statisticsProjecId").val();
        var url = "/statistics/project-group/" + projectGroupId;

        if (projectId) {
            url += "?projectId=" + projectId;
        }

        $.getJSON(url,
            function (data) {
                console.log(data);

                if (data["totalEstimations"] > 0) {
                    createScatterChart(data["daysEstimatedVsActualDataSet"], data["daysEstimatedVsActualDataSetXLabel"], data["daysEstimatedVsActualDataSetYLabel"]);

                    var pieDataSet = [
                        data["accurateEstimations"],
                        data["overEstimations3Days"],
                        data["underEstimations3Days"],
                        data["overEstimationsMoreThan3Days"],
                        data["underEstimationsMoreThan3Days"]
                    ];

                    createPieChart(pieDataSet);

                    $("#totalEstimations").html(data["totalEstimations"]);
                    $("#accuracyPercentage").html(data["accurateEstimations"] / data["totalEstimations"] * 100);

                    $('.stats-container').show();
                } else {
                    $('.stats-message').html("No statistics available").show();
                }
            }
        ).fail(function () {

        });
    });

    function createScatterChart(dataSet, xLabel, yLabel) {
        var ctx = document.getElementById('scatterChart');
        var scatterChart = new Chart(ctx, {
            type: 'scatter',
            data: {
                datasets: [{
                    label: "Task estimation completion (days) offset versus actual completion (days)",
                    data: dataSet,
                    xAxisID: "xAxis",
                    yAxisID: "yAxis",
                    backgroundColor: 'rgb(255, 99, 132)'
                }]
            },
            options: {
                scales: {
                    xAxes: [{
                        display: true,
                        id: "xAxis",
                        scaleLabel: {
                            display: true,
                            labelString: xLabel,
                        },
                        ticks: {
                            beginAtZero: true
                        }
                    }],
                    yAxes: [{
                        display: true,
                        id: "yAxis",
                        scaleLabel: {
                            display: true,
                            labelString: yLabel,
                        },
                        ticks: {
                            min: -20,
                            max: 20
                        }
                    }]
                }
            }
        });
    }

    function createPieChart(dataSet) {
        var ctx = document.getElementById('pieChart');
        var pieChart = new Chart(ctx, {
            type: 'pie',
            data: {
                datasets: [{
                    data: dataSet,
                    backgroundColor: [
                        'rgb(255, 99, 132)',
                        'rgb(54, 162, 235)',
                        'rgb(255, 206, 86)',
                        'rgb(75, 192, 192)',
                        'rgb(153, 102, 255)',
                        'rgb(255, 159, 64)'
                    ]
                }],
                labels: [
                    "Accurate estimations",
                    "Over estimations (1 to 3 days)",
                    "Under estimations (1 to 3 days)",
                    "Over estimations (more than 3 days)",
                    "Under estimations (more than 3 days)"
                ]
            }
        });
    }
})(jQuery);
