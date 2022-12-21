# CityDistanceCalculator

A distância entre várias cidades pode ser expressada por uma tabela, conforme a imagem abaixo.

![IMG](https://s3-sa-east-1.amazonaws.com/lcpi/5bc5db0a-bd2d-4689-9fbd-926f47ebeb64.png)

Implemente um programa que:

- Leia a tabela acima em um array bidimensional. O programa não deve perguntar distâncias já informadas (por exemplo, se o usuário já forneceu a distância entre 1 e 3 não é necessário informar a distância entre 3 e 1, que é a mesma) e também não deve perguntar a distância entre uma cidade e ela mesma, que é sempre 0;

- Leia um percurso fornecido pelo usuário e armazene em um array unidimensional.

- Após isso, calcule e mostre a distância percorrida pelo usuário. Por exemplo, para o percurso 1, 2, 3, 2, 5, 1, 4 teremos 15 + 10 + 10+ 28 + 12 + 5 = 80 km.