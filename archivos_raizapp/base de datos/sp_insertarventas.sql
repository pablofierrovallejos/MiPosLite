CREATE DEFINER=`pablo`@`%` PROCEDURE `sp_insertarVenta`(
  IN totalmonto     VARCHAR(45),
  IN totalproductos VARCHAR(45),
  IN fechaventa     VARCHAR(20),
  IN cuadratura     VARCHAR(45),
  OUT idout	 INT)
BEGIN

DECLARE imsecuencia INTEGER DEFAULT 0;
SELECT  id INTO  imsecuencia FROM  sequence;
UPDATE sequence SET id=LAST_INSERT_ID(id+1);

set idout = imsecuencia;

INSERT INTO `mipos`.`ventas`
        (`idventa`,`totalmonto`,`totalproductos`,`fechaventa`,`cuadratura`)
VALUES (imsecuencia,totalmonto, totalproductos, fechaventa, cuadratura);
commit;

END