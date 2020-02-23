--USE [���ݿ�����]
--GO

--IF EXISTS (SELECT * FROM sys.symmetric_keys WHERE name='SymmetricByAsy')
DROP SYMMETRIC KEY SymmetricByAsy
--IF EXISTS (SELECT * FROM sys.symmetric_keys WHERE name='SymmetricBySy')
DROP SYMMETRIC KEY SymmetricBySy
--IF EXISTS (SELECT * FROM sys.symmetric_keys WHERE name='JinHuiSymmetric')
DROP SYMMETRIC KEY JinHuiSymmetric
--IF EXISTS (SELECT * FROM sys.symmetric_keys WHERE name='SymmetricByCert')
DROP SYMMETRIC KEY SymmetricByCert
--IF EXISTS (SELECT * FROM sys.asymmetric_keys WHERE name='JinHuiAsymmetric')
DROP ASYMMETRIC KEY JinHuiAsymmetric
--IF EXISTS (SELECT * FROM sys.certificates WHERE name='JinHuiCertificate')
DROP CERTIFICATE JinHuiCertificate 
--select name,is_master_key_encrypted_by_server from sys.databases
--IF EXISTS (SELECT * FROM sys.databases 
--WHERE name='master' AND is_master_key_encrypted_by_server='1')
DROP MASTER KEY 

---------------------�����Ҫɾ������֤�飬ֻ��ִ�����ϵĴ���---------------------

--�������ݿ�����Կ
-- IF EXISTS (SELECT is_master_key_encrypted_by_server FROM sys.databases 
-- WHERE name='master' AND is_master_key_encrypted_by_server='1')
CREATE MASTER KEY ENCRYPTION BY PASSWORD ='JinHui2019'
--����֤��
CREATE CERTIFICATE JinHuiCertificate 
with SUBJECT = 'JinHuiCertificate2019';

--�����ǶԳ���Կ
CREATE ASYMMETRIC KEY JinHuiAsymmetric
    WITH ALGORITHM = RSA_2048 
    ENCRYPTION BY PASSWORD = 'JinHui2019'; 

--�����Գ���Կ
CREATE SYMMETRIC KEY JinHuiSymmetric
    WITH ALGORITHM = AES_256
    ENCRYPTION BY PASSWORD = 'JinHui2019';


--�ڶ���
--��֤����ܶԳ���Կ
CREATE SYMMETRIC KEY SymmetricByCert
    WITH ALGORITHM = AES_256
    ENCRYPTION BY CERTIFICATE JinHuiCertificate;


--�ɶԳ���Կ���ܶԳ���Կ
OPEN SYMMETRIC KEY JinHuiSymmetric
    DECRYPTION BY PASSWORD='JinHui2019';

CREATE SYMMETRIC KEY SymmetricBySy
    WITH ALGORITHM = AES_256
    ENCRYPTION BY SYMMETRIC KEY JinHuiSymmetric;


--�ɷǶԳ���Կ���ܶԳ���Կ
CREATE SYMMETRIC KEY SymmetricByAsy
    WITH ALGORITHM = AES_256
    ENCRYPTION BY ASYMMETRIC KEY JinHuiASymmetric;
