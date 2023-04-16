document.addEventListener('DOMContentLoaded', () => {

  const equipamentos = document.querySelectorAll('.equipamento');
  const botaoConfirma = document.querySelector('.botao-confirma');
  const checkboxTermo = document.querySelector('#termo');
  const botaoTiraFoto = document.querySelector('#tirar-foto');

  // Event listener para o botão de confirmação
  botaoConfirma.addEventListener('click', (event) => {
    event.preventDefault(); // previne que a página seja atualizada
    const checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');
    if (checkboxes.length < 2 || !checkboxTermo.checked || (botaoTiraFoto && !botaoTiraFoto.classList.contains('foto-tirada'))) {
      alert('Preencha pelo menos duas EPIs, aceite o termo e tire uma foto para continuar');
      return;
    } else {
      alert('Dados enviados!');
      window.scrollTo({ top: 0, left: 0, behavior: 'smooth' });
      const mensagemConfirmacao = document.createElement('p');
      mensagemConfirmacao.textContent = 'Dados enviados!';
      document.querySelector('body').appendChild(mensagemConfirmacao);
    }
  });

  // Event listener para os checkboxes
  equipamentos.forEach(equipamento => {
    equipamento.addEventListener('click', () => {
      const checkbox = equipamento.querySelector('input[type="checkbox"]');
      checkbox.checked = !checkbox.checked;
    });
  });

});
