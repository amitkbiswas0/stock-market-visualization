$(document).ready(function () {
  loadDataTable();
});

// load left table
function loadDataTable() {
  $("#DT_load").DataTable({
    ajax: {
      url: "/api/Chart",
      type: "GET",
      datatype: "json",
    },
    columns: [
      { data: "tradeCode", width: "80%" },
      {
        data: "tradeCode",
        render: function (data) {
          // returns trade code and button
          return `<div class="text-center">
                    <a onclick=RenderChart('/api/Chart/${data}') class='btn btn-success text-white' style='cursor:pointer;'>></a>
                  </div>`;
        },
        width: "20%",
      },
    ],
    language: {
      emptyTable: "No Data Found!",
    },
    pagingType: "numbers",
    lengthChange: false,
    info: false,
    width: "100%",
  });
  // for removing length Change property of table
  document.querySelector(
    "#DT_load_wrapper"
  ).firstChild.firstChild.style.display = "none";
}

// api call for chart values
function grab(url) {
  return new Promise((resolve, reject) => {
    $.ajax({
      url: url,
      method: "GET",
      dataType: "JSON",
      success: function (data) {
        resolve(data);
      },
      error: function (error) {
        reject(error);
      },
    });
  });
}

function RenderChart(url) {
  // remove old chart container
  const parent = document.getElementById("parent");
  parent.removeChild(document.getElementById("chartContainer"));

  // do things after receiving values
  grab(url)
    .then((res) => {
      try {
        const dataPoints = [];
        res.data.forEach((item) => {
          dataPoints.push({
            x: new Date(
              parseInt(item.tradeDate.split("-")[0]),
              parseInt(item.tradeDate.split("-")[1]),
              parseInt(item.tradeDate.split("-")[2])
            ),
            y: [
              parseFloat(item.open),
              parseFloat(item.high),
              parseFloat(item.low),
              parseFloat(item.close),
            ],
          });
        });
        // add new charts container, has two chart
        const newElement = document.createElement("div");
        newElement.setAttribute("id", "chartContainer");
        newElement.setAttribute("class", "col-8");
        newElement.innerHTML = `
        <div id="chartContainer1" class="col-12"></div>
        <div id="chartContainer2" class="col-12" style="margin-top: 350px;"></div>
        `;
        parent.appendChild(newElement);

        // render OHLC Chart
        const chart = new CanvasJS.Chart("chartContainer1", {
          animationEnabled: true,
          theme: "light2",
          title: {
            text: "Stock Market Details",
            padding: 1,
          },
          height: 300,
          axisX: {
            interval: 1,
            valueFormatString: "MMM",
          },
          axisY: {
            prefix: "$",
            title: "Price",
          },
          toolTip: {
            content: `Date: {x}<br />
              <strong>Price:</strong><br />
              Open: {y[0]}, High: {y[1]}<br />
              Low: {y[2]}, Close: {y[3]}`,
          },
          data: [
            {
              type: "candlestick",
              yValueFormatString: "$##0.00",
              dataPoints: dataPoints,
            },
          ],
        });
        chart.render();

        // create and render value chart
        const values = [];
        res.data.forEach((item) => {
          values.push({
            x: new Date(
              parseInt(item.tradeDate.split("-")[0]),
              parseInt(item.tradeDate.split("-")[1]),
              parseInt(item.tradeDate.split("-")[2])
            ),
            // converts string to int
            y: parseInt(item.volume.match(/\d/g).join("")),
          });
        });
        const vChart = new CanvasJS.Chart("chartContainer2", {
          title: {
            text: "Values of Stocks",
            padding: 1,
          },
          axisY: {
            prefix: "$",
            title: "Volume",
          },
          toolTip: {
            content: `Date: {x}<br />
              <strong>Volume:</strong> {y}`,
          },
          data: [
            {
              type: "line",
              dataPoints: values,
            },
          ],
        });
        vChart.render();
      } catch (error) {
        console.log("Error parsing JSON!", error);
      }
    })
    .catch((error) => {
      console.log(error);
    });
}
