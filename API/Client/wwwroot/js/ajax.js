$('#ajaxSW').DataTable({
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
        { extend: 'excel'},
        { extend: 'print' }
    ],
    columns: [
        { data: 'nik' },
        {
            data: 'firstName',
            render: function (data, type, row) {
                return row.firstName + '&nbsp' + row.lastName ;
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
    console.log(result.result);
    text = "<option selected>Select University</option>";
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
        console.log(result.result);
        text = "";
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
    $("#formReg").submit(function (event) {
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
        //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
        $.ajax({
            url: "https://localhost:44371/API/Employees/Register",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(obj)
        }).done((result) => {
            //buat alert pemberitahuan jika success
            alert(result.message);
        }).fail((error) => {
            //alert pemberitahuan jika gagal
            alert(error.responseJSON.message);
        })
    })

}