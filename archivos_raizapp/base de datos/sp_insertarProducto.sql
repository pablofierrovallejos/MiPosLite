CREATE DEFINER=`pablo`@`%` PROCEDURE `sp_insertarProducto`(
IN nombreproducto     VARCHAR(45),
IN precio             VARCHAR(20),
IN fechacreacion      VARCHAR(10),
IN vigente            VARCHAR(1),
IN nroposicion        VARCHAR(45), 
IN cantidaddisponible VARCHAR(10) 
)
BEGIN

DECLARE imsecuencia INTEGER DEFAULT 0;
SELECT  id INTO  imsecuencia FROM  sequence;
UPDATE sequence SET id=LAST_INSERT_ID(id+1);

INSERT INTO `mipos`.`productos`
(`idproductos`,
`nombreproducto`,
`precio`,
`fechacreacion`,
`vigente`,
`nroposicion`,
`cantidaddisponible`)
VALUES
(imsecuencia,
nombreproducto,
precio,
fechacreacion,
vigente,
nroposicion,
cantidaddisponible);
commit;
END