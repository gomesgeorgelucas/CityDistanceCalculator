# CityDistanceCalculator

A dist�ncia entre v�rias cidades pode ser expressada por uma tabela, conforme a imagem abaixo.

![IMG](https://s3-sa-east-1.amazonaws.com/lcpi/5bc5db0a-bd2d-4689-9fbd-926f47ebeb64.png)

Implemente um programa que:

- Leia a tabela acima em um array bidimensional. O programa n�o deve perguntar dist�ncias j� informadas (por exemplo, se o usu�rio j� forneceu a dist�ncia entre 1 e 3 n�o � necess�rio informar a dist�ncia entre 3 e 1, que � a mesma) e tamb�m n�o deve perguntar a dist�ncia entre uma cidade e ela mesma, que � sempre 0;

- Leia um percurso fornecido pelo usu�rio e armazene em um array unidimensional.

- Ap�s isso, calcule e mostre a dist�ncia percorrida pelo usu�rio. Por exemplo, para o percurso 1, 2, 3, 2, 5, 1, 4 teremos 15 + 10 + 10+ 28 + 12 + 5 = 80 km.