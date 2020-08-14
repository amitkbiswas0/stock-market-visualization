let dataTable;

$(document).ready(function () {
  loadDataTable();
});

function loadDataTable() {
  dataTable = $("#DT_load").DataTable({
    ajax: {
      url: "/api/Stock",
      type: "GET",
      datatype: "json",
    },
    columns: [
      { data: "tradeCode", width: "20%" },
      { data: "tradeDate", width: "30%" },
      { data: "open", width: "5%" },
      { data: "high", width: "5%" },
      { data: "low", width: "5%" },
      { data: "close", width: "5%" },
      { data: "volume", width: "5%" },
      {
        data: "id",
        render: function (data) {
          return `<div class="text-center">
                    <a href="/CustomPages/Edit?id=${data}" class='btn btn-success text-white' style='cursor:pointer;'>Edit</a>
                    <a class='btn btn-danger text-white' style='cursor:pointer;'onclick=Delete('/api/Stock?id=${data}')>Delete</a>
                </div>`;
        },
        width: "25%",
      },
    ],
    language: {
      emptyTable: "No Data Found!",
    },
    width: "100%",
  });
}

function Delete(url) {
  swal({
    title: "Are you sure?",
    text: "Once deleted, Record can not be recovered!",
    icon: "warning",
    buttons: true,
    dangerMode: true,
  }).then((willDelete) => {
    if (willDelete) {
      $.ajax({
        type: "DELETE",
        url: url,
        success: function (data) {
          if (data.success) {
            toastr.success(data.message);
            dataTable.ajax.reload();
          } else {
            toastr.error(data.message);
          }
        },
      });
    }
  });
}
