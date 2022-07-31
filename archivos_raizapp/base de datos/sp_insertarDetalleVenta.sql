CREATE DEFINER=`pablo`@`%` PROCEDURE `sp_insertarDetalleVenta`(
  IN iddetalleventas INTEGER,
  IN idproducto VARCHAR(45),
  IN cantidad   VARCHAR(45),
  IN total      VARCHAR(45)
)
BEGIN
DECLARE imsecuencia INTEGER DEFAULT 0;
SELECT  id INTO  imsecuencia FROM  sequence;
UPDATE sequence SET id=LAST_INSERT_ID(id+1);


INSERT INTO `mipos`.`detalleventas`
(`idregistro`,
`iddetalleventas`,
`idproducto`,
`cantidad`,
`total`)
VALUES
(imsecuencia,
iddetalleventas,
idproducto,
cantidad,
total);
commit;
END