$.extend($.fn.dataTable.defaults, {
    responsive: true
});

var table = $('#ajaxSW').DataTable({
    ajax: {
        url: '/employee/GetRegistrasiView/',
        dataSrc: ''
    },
    dom: 'Bfrtip',
    buttons: [
        {
            extend: 'pdf',
            orientation: 'landscape'
        },
        { extend: 'excel' },
        { extend: 'print' }
    ],
    columns: [
        { data: 'nik' },
        {
            data: 'firstName',
            render: function (data, type, row) {
                return row.firstName + '&nbsp' + row.lastName;
            }
        },
        { data: 'email' },
        {
            data: 'gender',
            render: function (data, type, row) {
                if (data === 0) {
                    return "Pria";
                }
                else {
                    return "Wanita";
                }
            }
        },
        { data: 'salary' },
        {
            data: 'birthDate',
            render: function (data, type, row) {
                return data.split('T')[0];
            }
        },
        {
            data: 'phoneNumber',
            render: function (data, type, row) {
                return "+62" + data.slice(1)
            }
        },
        { data: 'degree' },
        { data: 'gpa' },
        { data: 'universitas' },
        {
            data: null,
            targets: 'no-sort', orderable: false,
            render: function (data, type, row) {
                return "<button  class=\"btn btn-primary\" >Edit</button>";
            }
        }
    ]
});

/*Menampilkan universitas*/
$.ajax({
    url: "https://localhost:44371/api/universities"
}).done((result) => {
    text = "<option selected disabled value=\"\">Choose...</option>";
    no = 1;
    $.each(result.result, function (key, val) {
        text += `<option value="${val.id}">${val.name}</option>`;
    });
    $("#univ").html(text);
}).fail((error) => {
    console.log(error);
});

/*menampilkan degree dari education*/
$('#univ').change(function () {
    let univ = $(this).val();
    $.ajax({
        url: "https://localhost:44371/api/educations"
    }).done((result) => {
        text = "<option selected disabled value=\"\">Choose...</option>";
        $.each(result.result, function (key, val) {
            if (univ == val.universityId) {
                text += `<option value="${val.degree}">${val.degree}</option>`;
            }
        });
        $("#educt").html(text);
    }).fail((error) => {
        console.log(error);
    });
});

/*Melakukan insert registrasi*/
function Insert() {
    var obj = new Object();
    obj.NIK = $("#nik").val();
    obj.Password = $("#password").val();
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Gender = parseInt($("input[name=gender]:checked").val());
    obj.BirthDate = $("#birthDate").val();
    obj.PhoneNumber = $("#phoneNumber").val();
    obj.Email = $("#email").val();
    obj.Salary = parseInt($("#salary").val());
    obj.universityId = parseInt($("#univ").val());
    obj.degree = $("#educt").val();

    console.log(obj);
    $.ajax({
        url: "https://localhost:44371/API/Employees/Register",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj)
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: result.message,
        });
        $('#regisModal').modal('toggle');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        table.ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Registrasi Gagal',
            text: error.responseJSON.message,
        });
    })

}

window.addEventListener('load', () => {
    var forms = document.getElementsByClassName('needs-validation');
    for (let form of forms) {
        form.addEventListener('submit', (evt) => {
            if (!form.checkValidity()) {
                evt.preventDefault();
                evt.stopPropagation();
            } else {
                evt.preventDefault();
                Insert();
            }
            form.classList.add('was-validated');
        });
    }
});

let pria = countGender(0) ;
let wanita = countGender(1);

var options = {
    chart: {
        type: 'donut',
        height: '400px'
    },
    dataLabels: {
        enabled: false
    },
    series: [pria, wanita],
    labels: ['pria','wanita'],
    noData: {
        text: 'Loading...'
    }
}

var chart = new ApexCharts(document.querySelector("#chart"), options);

chart.render();

function countGender(gender) {
    let count = 0;
    jQuery.ajax({
        url: '/employee/GetRegistrasiView/',
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.gender === gender) {
                    ++count;
                }
            });
        },
        async: false
    });
    return count;
}


let uniA = countUniv("Universitas A");
let uniB = countUniv("Universitas B");
let uniC = countUniv("Universitas C");

var options = {
    chart: {
        type: 'bar',
        height: '253px'
    },
    series: [{
        name: 'employee',
        data: [uniA, uniB, uniC]
    }],
    xaxis: {
        categories: ["Universitas A", "Universitas B", "Universitas C"]
    }
}
var barChart = new ApexCharts(document.querySelector("#barChart"), options);
barChart.render();

function countUniv(univ) {
    let count = 0;
    jQuery.ajax({
        url: '/employee/GetRegistrasiView/',
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.universitas === univ) {
                    ++count;
                }
            });
        },
        async: false
    });
    return count;
}
