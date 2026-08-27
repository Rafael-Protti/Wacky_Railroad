# Instruções

## Nível 1

Crie uma cena para o nível principal.

### Percurso com os trilhos

1. Gere 3 trilhos, assim como está descrito no **GDD**, onde a locomotiva (jogador) vai se movimentar.

2. Crie um percurso com os trilhos que se inicia em linha reta e faz curvas eventualmente.

3. Os trilhos devem obrigatóriamente serem feitos com o pacote `Spline` da Unity que está instalado no projeto.

### Posicione Obstáculos

1. Crie os tipos principais de obstáculos: Baixos, Altos e Leves.

2. Posicione-os aleatoriamente nos trilhos, de forma que o jogador possa desviar deles.

### Jogador

1. Crie a locomotiva do jogador.

2. A locomotiva deve ter o Script do `Spline` que permite sua movimentação nos trilhos.

3. Crie o script de movimento do jogador, e aplique no objeto da locomotiva.

4. A movimentação do jogador deve seguir as segras do **GDD** e conversar com as funções do `Spline`.

5. Aplique todos os componetes necessários na locomotiva.

6. Aplique um vagão na locomotiva, que deve se ligar a ela com o componente `Fixed Joint`.

7. Faça que o jogador volte para o começo da fase ao colidir com algum obstáculo.

