<template>
  <v-container>
    <painel-acoes-confirmar-cancelar
      @confirmar="confirmar"
      @cancelar="cancelar"
    ></painel-acoes-confirmar-cancelar>

    <v-card class="ma-5">
      <v-tabs v-model="tabSelected">
        <v-tab href="#geral">Geral</v-tab>
        <v-tab href="#features">Features</v-tab>
        <v-tab href="#log">Log</v-tab>

        <v-tabs-items v-model="tabSelected">
          <v-tab-item value="geral">
            <v-card class="ma-5">
              <v-card-text class="pa-0">
                <v-app-bar flat class="gray lighten-4 pa-0"
                  >Informações Gerais</v-app-bar
                >
                <v-container>
                  <v-row>
                    <v-col>
                      <v-text-field
                        v-model="projeto.id"
                        readonly
                        filled
                        label="Id"
                      ></v-text-field>
                    </v-col>

                    <v-col>
                      <v-text-field
                        v-model="projeto.nome"
                        counter="100"
                        hint=""
                        label="Nome"
                      ></v-text-field>
                    </v-col>
                  </v-row>

                  <v-row>
                    <v-col>
                      <v-text-field
                        v-model="projeto.descricao"
                        counter="500"
                        hint=""
                        label="Descrição"
                      ></v-text-field>
                    </v-col>
                  </v-row>

                  <v-row>
                    <v-col>
                      <v-text-field
                        v-model="projeto.criadoPor"
                        label="Criado por"
                        readonly
                        filled
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        v-model="projeto.criadoEm"
                        label="Criado Em"
                        readonly
                        filled
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        v-model="projeto.atualizadoPor"
                        label="Atualizado por"
                        readonly
                        filled
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        v-model="projeto.atualizadoEm"
                        label="Atualizado Em"
                        readonly
                        filled
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>
            </v-card>

            <v-card class="ma-5">
              <v-card-text class="pa-0">
                <v-app-bar flat class="gray lighten-4 pa-0"
                  >Dados do Projeto</v-app-bar
                >
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
          </v-tab-item>

          <v-tab-item value="features">
            <v-card class="ma-5">
              <v-card-text class="pa-0">
                <v-app-bar flat class="gray lighten-4 pa-0">Features</v-app-bar>
                <v-simple-table>
                  <template v-slot:default>
                    <thead>
                      <tr>
                        <th class="text-left">Nome</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="feature in features" :key="feature.id">
                        <td>{{ feature.nome }}</td>
                      </tr>
                    </tbody>
                  </template>
                </v-simple-table>
              </v-card-text>
            </v-card>
          </v-tab-item>

          <v-tab-item value="log">
            <v-card class="ma-5">
              <v-card-text class="pa-0">
                <v-app-bar flat class="gray lighten-4 pa-0"
                  >Log (Horário de Brasília)</v-app-bar
                >

                <v-card v-for="log in logs" :key="log.id" class="ma-2">
                  <v-card-title>{{ log.alteradoEm | filtroData }}</v-card-title>
                  <v-card-text>
                    O valor do campo
                    <span class="log-destaque">{{ log.nomeCampo }}</span> foi
                    preenchido como
                    <span class="log-destaque">{{ log.valorNovo }}</span> por
                    <span>{{ log.alteradoPor.nome | uppercase }}</span>
                  </v-card-text>
                  <v-card-subtitle
                    ><strong>Valor antigo:</strong>
                    {{ log.valorAntigo }}</v-card-subtitle
                  >
                </v-card>
              </v-card-text>
            </v-card>
          </v-tab-item>
        </v-tabs-items>
      </v-tabs>
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
import logSistemaService from "./../../apis/logSistemaService";
import featureFlagService from "./../../apis/featureFlagService";
import { filtroData } from "../../filters/filtro-data";

import PainelAcoesConfirmarCancelar from "./../../components/PainelAcoesConfirmarCancelar";

export default {
  mixins: [utils],
  filters: { filtroData },
  components: {
    PainelAcoesConfirmarCancelar
  },
  data() {
    return {
      snackbar: false,
      snackbarMessage: "",
      snackbarTimeout: 2000,
      tabSelected: "",
      projeto: {
        nome: "",
        descricao: "",
        urlJira: "",
      },
      features: [],
      logs: [],
    };
  },
  methods: {
    async confirmar() {
      const projeto = {
        nome: this.projeto.nome,
        descricao: this.projeto.descricao,
        urlJira: this.projeto.urlJira,
      };

      await projetoService.update(this.lerToken(), this.projeto.id, projeto);

      this.snackbarMessage = "O projeto foi editado com sucesso";
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
  async created() {
    const id = this.$route.params.id;

    this.projeto = await projetoService.get(this.lerToken(), id);
    this.logs = await logSistemaService.list(
      this.lerToken(),
      this.projeto.idLogReference
    );
    this.features = await featureFlagService.list(this.lerToken(), this.projeto.id);
  },
};
</script>

<style scoped>
.log-destaque {
  font-weight: bold;
  font-style: italic;
}
</style>