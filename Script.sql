ALTER TABLE [dbo].[CLIENTES]
    ADD [CPF] VARCHAR (11) NULL;

GO

----------------------------------------------------------------------------------------------------------------------

GO
CREATE OR ALTER PROC FI_SP_AltCliente
    @NOME          VARCHAR (50) ,
    @SOBRENOME     VARCHAR (255),
    @NACIONALIDADE VARCHAR (50) ,
    @CEP           VARCHAR (9)  ,
    @ESTADO        VARCHAR (2)  ,
    @CIDADE        VARCHAR (50) ,
    @LOGRADOURO    VARCHAR (500),
    @EMAIL         VARCHAR (2079),
    @TELEFONE      VARCHAR (15),
	@CPF		   VARCHAR (11),
	@Id           BIGINT
AS
BEGIN
	UPDATE CLIENTES 
	SET 
		NOME = @NOME, 
		SOBRENOME = @SOBRENOME, 
		NACIONALIDADE = @NACIONALIDADE, 
		CEP = @CEP, 
		ESTADO = @ESTADO, 
		CIDADE = @CIDADE, 
		LOGRADOURO = @LOGRADOURO, 
		EMAIL = @EMAIL, 
		TELEFONE = @TELEFONE,
		CPF = @CPF
	WHERE Id = @Id
END
GO

----------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE [dbo].[FI_SP_DelBenef]
	@ID BIGINT
AS
BEGIN
	DELETE BENEFICIARIOS WHERE ID = @ID
END

----------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROC FI_SP_IncClienteV2
    @NOME          VARCHAR (50) ,
    @SOBRENOME     VARCHAR (255),
    @NACIONALIDADE VARCHAR (50) ,
    @CEP           VARCHAR (9)  ,
    @ESTADO        VARCHAR (2)  ,
    @CIDADE        VARCHAR (50) ,
    @LOGRADOURO    VARCHAR (500),
    @EMAIL         VARCHAR (2079),
    @TELEFONE      VARCHAR (15),
	@CPF		   VARCHAR (11)
AS
BEGIN
	INSERT INTO CLIENTES (NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, CPF) 
	VALUES (@NOME, @SOBRENOME,@NACIONALIDADE,@CEP,@ESTADO,@CIDADE,@LOGRADOURO,@EMAIL,@TELEFONE, @CPF)

	SELECT SCOPE_IDENTITY()
END
GO

----------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE [dbo].[FI_SP_PesqBeneficiarioByCli]
	@id_cliente bigint
AS
BEGIN
	SELECT NOME, CPF, ID FROM BENEFICIARIOS where IDCLIENTE = @id_cliente
END
GO

----------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROC FI_SP_PesqCliente
	@iniciarEm int,
	@quantidade int,
	@campoOrdenacao varchar(200),
	@crescente bit	
AS
BEGIN
	DECLARE @SCRIPT NVARCHAR(MAX)
	DECLARE @CAMPOS NVARCHAR(MAX)
	DECLARE @ORDER VARCHAR(50)
	
	IF(@campoOrdenacao = 'EMAIL')
		SET @ORDER =  ' EMAIL '
	ELSE
		SET @ORDER = ' NOME '

	IF(@crescente = 0)
		SET @ORDER = @ORDER + ' DESC'
	ELSE
		SET @ORDER = @ORDER + ' ASC'

	SET @CAMPOS = '@iniciarEm int,@quantidade int'
	SET @SCRIPT = 
	'SELECT ID, NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, CPF FROM
		(SELECT ROW_NUMBER() OVER (ORDER BY ' + @ORDER + ') AS Row, ID, NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, CPF FROM CLIENTES WITH(NOLOCK))
		AS ClientesWithRowNumbers
	WHERE Row > @iniciarEm AND Row <= (@iniciarEm+@quantidade) ORDER BY'
	
	SET @SCRIPT = @SCRIPT + @ORDER
			
	EXECUTE SP_EXECUTESQL @SCRIPT, @CAMPOS, @iniciarEm, @quantidade

	SELECT COUNT(1) FROM CLIENTES WITH(NOLOCK)
END
GO

----------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROC FI_SP_VerificaCliente
	@CPF VARCHAR(11)
AS
BEGIN
	SELECT 1 FROM CLIENTES WHERE CPF = @CPF
END
GO

----------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROC FI_SP_ConsCliente
	@ID BIGINT
AS
BEGIN
	IF(ISNULL(@ID,0) = 0)
		SELECT NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, ID, CPF FROM CLIENTES WITH(NOLOCK)
	ELSE
		SELECT NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, ID, CPF FROM CLIENTES WITH(NOLOCK) WHERE ID = @ID
END
GO

----------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE [dbo].[FI_SP_IncBeneficiario]
	@CPF VARCHAR(11),
	@NOME VARCHAR(50),
	@ID_CLIENTE BIGINT
AS
BEGIN
	INSERT INTO BENEFICIARIOS (CPF, NOME, IDCLIENTE)
	VALUES (@CPF, @NOME, @ID_CLIENTE)

	SELECT SCOPE_IDENTITY()
END
GO

----------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE [dbo].[FI_SP_VerificaBeneficiarioExiste]
	@ID BIGINT
AS
BEGIN
	SELECT 1 FROM BENEFICIARIOS WHERE ID = @ID
END
GO