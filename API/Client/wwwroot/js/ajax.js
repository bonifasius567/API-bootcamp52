donat();
countUniv();
//=============================================================================================
//=================================Show employee data table====================================
//=============================================================================================

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
            data: 'nik',
            targets: 'no-sort', orderable: false,
            render: function (data, type, row) {
                return `<button value="${data}" class="btn btn-primary" >Edit</button> &nbsp
                        <button value="${data}" onclick="delEmployee(this.value)" class="btn btn-danger">Delete</button> `;
            }
        }
    ]
});

//=============================================================================================
//=================================add option university=======================================
//=============================================================================================

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

//=============================================================================================
//=================================add option degree===========================================
//=============================================================================================

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

//=============================================================================================
//=================================insert new registration=====================================
//=============================================================================================

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
        table.ajax.reload(null, false);
        donat();
        $('body').removeClass('modal-open');
        $('#regisModal').modal('hide');
        $('.modal-backdrop').remove();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Registrasi Gagal',
            text: error.responseJSON.message,
        });
    })
}

//=============================================================================================
//=================================Add Validasi bootstrap======================================
//=============================================================================================

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


//=============================================================================================
//=================================show  donutChart for Gender=================================
//=============================================================================================

function donat() {
    pria = 0;
    wanita = 0;
    $.ajax({
        url: '/employee/GetRegistrasiView/',
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.gender == 0) {
                    pria++;
                }
                else {
                    wanita++;
                }
            });
            var options = {
                chart: {
                    type: 'donut',
                    height: 312
                },
                dataLabels: {
                    enabled: false
                },
                series: [pria, wanita],
                labels: ['pria', 'wanita'],
                noData: {
                    text: 'Loading...'
                }
            }

            var chart = new ApexCharts(document.querySelector("#chart"), options);

            chart.render();
        },
        async: false
    })
}

//=============================================================================================
//=================================show  barchart for university===============================
//=============================================================================================

function countUniv() {
    let uniA = null;
    let uniB = null;
    let uniC = null;
    jQuery.ajax({
        url: '/employee/GetRegistrasiView/',
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.universitas == "Universitas A") {
                    uniA++;
                } else if (val.universitas == "Universitas B") {
                    uniB++;
                } else {
                    uniC++;
                }
            });
            var options = {
                chart: {
                    type: 'bar',
                    height: 300
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
        },
        async: false
    });
}

//=============================================================================================
//=================================delete any registration=====================================
//=============================================================================================

function delEmployee(del) {
    $.ajax({
        url: "https://localhost:44371/API/AccountRoles"
    }).done((result) => {
        $.each(result.result, function (key, val) {
            console.log(del);
            if (val.accountId == del) {
                console.log(val.accountId);
                Swal.fire({
                    title: 'Are you sure?',
                    text: "You won't be able to revert this!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, delete it!'
                }).then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: "https://localhost:44371/API/AccountRoles?key=" + val.id,
                            type: 'delete'
                        });
                        $.ajax({
                            url: "https://localhost:44371/API/Employees?Key=" + del,
                            type: 'delete'
                        }).done((result) => {
                            Swal.fire({
                                icon: 'success',
                                title: 'Deleted.',
                                text: result.message,
                            });
                            table.ajax.reload();
                        }).fail((error) => {
                            Swal.fire({
                                icon: 'error',
                                title: 'Failed',
                                text: error.responseJSON.message,
                            });
                        })
                    }
                })
            }
        });
    })

}

