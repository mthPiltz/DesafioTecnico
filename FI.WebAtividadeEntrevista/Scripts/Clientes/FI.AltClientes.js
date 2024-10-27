let beneficiarios = [];
$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #Cpf').val(obj.Cpf);
    }

    $('#formBeneficiarios').submit(function (e) {
        e.preventDefault();

        const nome = $(this).find("#NomeBeneficiario").val();
        const cpf = $(this).find("#CpfBeneficiario").val();

        beneficiarios.push({
            nome: nome,
            cpf: cpf
        });

        adicionarBeneficiario(nome, cpf);
    })

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF": $(this).find("#Cpf").val()
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();                                
                window.location.href = urlRetorno;
            }
        });
    })

    $("#Cpf").on('keydown paste', function () {
        mascararCpf(this)
    });

    $("#CpfBeneficiario").on('keydown', function () {
        mascararCpf(this);
    })

    $("#btnBeneficiario").on('click', function () {
        $("#modalBeneficiarios").css({
            "display": "block"
        });
    });

    $('#btnFecharModal').on('click', function () {
        $("#modalBeneficiarios").css({
            "display": "none"
        });
    })
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}


function mascararCpf(campo) {
    let valor = campo.value.replace(/\D/g, '');
    console.log(valor)
    if (valor.length > 11) {
        valor = valor.slice(0, 11);
    }

    if (valor.length <= 3) {
        campo.value = valor;
    } else if (valor.length <= 6) {
        campo.value = valor.slice(0, 3) + '.' + valor.slice(3);
    } else if (valor.length <= 9) {
        campo.value = valor.slice(0, 3) + '.' + valor.slice(3, 6) + '.' + valor.slice(6);
    } else {
        campo.value = valor.slice(0, 3) + '.' + valor.slice(3, 6) + '.' + valor.slice(6, 9) + '-' + valor.slice(9);
    }
} 


function adicionarBeneficiario(nome, cpf) {
    $('#listBeneficiarios').append(`
        <div class='row'>
            <div class="col-md-4">
                ${cpf}
            </div>
            <div class="col-md-4">
                ${nome}    
            </div>
            <div class="col-md-4" style='margin-top: 5px;'>
                <button type="button" class="btn btn-info">Alterar</button>
                <button type="button" class="btn btn-info">Excluir</button>
            </div>
        </div>
        <hr />
    `)
}