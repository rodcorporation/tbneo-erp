<template>
  <v-container>
    <painel-acoes-confirmar-cancelar
      @confirmar="confirmar"
      @cancelar="cancelar"
    ></painel-acoes-confirmar-cancelar>

    <v-card class="ma-5">
      <v-card-text class="pa-0">
        <v-app-bar flat class="gray lighten-4 pa-0"
          >Informações Gerais</v-app-bar
        >
        <v-container>
          <v-row>
            <v-col>
              <v-text-field
                v-model="projeto.nome"
                counter="100"
                hint=""
                label="Nome"
              ></v-text-field>
            </v-col>

            <v-col>
              <v-text-field
                v-model="projeto.descricao"
                counter="500"
                hint=""
                label="Descrição"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
    </v-card>

    <v-card class="ma-5">
      <v-card-text class="pa-0">
        <v-app-bar flat class="gray lighten-4 pa-0">Dados do Projeto</v-app-bar>
        <v-container>
          <v-row>
            <v-col>
              <v-text-field
                v-model="projeto.urlJira"
                counter="200"
                hint=""
                label="Url do Jira"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
    </v-card>

    <painel-acoes-confirmar-cancelar
      @confirmar="confirmar"
      @cancelar="cancelar"
    ></painel-acoes-confirmar-cancelar>

    <v-snackbar v-model="snackbar" :timeout="snackbarTimeout">
      {{ snackbarMessage }}
      <template v-slot:action="{ attrs }">
        <v-btn color="blue" text v-bind="attrs" @click="snackbar = false">
          Fechar
        </v-btn>
      </template>
    </v-snackbar>
  </v-container>
</template>

<script>
import utils from "../../mixins/utils";
import projetoService from "./../../apis/projetoService";

import PainelAcoesConfirmarCancelar from "./../../components/PainelAcoesConfirmarCancelar";

export default {
  mixins: [utils],
  components: {
    PainelAcoesConfirmarCancelar,
  },
  data() {
    return {
      snackbar: false,
      snackbarMessage: "",
      snackbarTimeout: 2000,
      projeto: {
        nome: "",
        descricao: "",
        urlJira: "",
      },
    };
  },
  methods: {
    async confirmar() {
      const projeto = {
        nome: this.projeto.nome,
        descricao: this.projeto.descricao,
        urlJira: this.projeto.urlJira,
      };

      await projetoService.add(this.lerToken(), projeto);

      this.snackbarMessage = "O projeto foi cadastrado com sucesso";
      this.snackbar = true;

      const _this = this;

      setTimeout(function () {
        _this.$router.push({ name: "projetoListagem" });
      }, 2000);
    },
    cancelar() {
      this.$router.push({ name: "projetoListagem" });
    },
  },
};
</script>