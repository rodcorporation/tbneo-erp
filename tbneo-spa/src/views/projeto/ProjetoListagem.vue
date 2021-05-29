<template>
  <v-container>
    <painel-acoes-novo @novo="cadastrar()"></painel-acoes-novo>

    <v-card class="ma-5">
      <v-card-title>Projetos</v-card-title>
      <v-card-text>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">Nome</th>
                <th class="text-left">Descrição</th>
                <th class="text-left">Atualizado por</th>
                <th class="text-left">Atualizado Em</th>
                <th class="text-left">Ações</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in resultado" :key="item.id">
                <td>{{ item.nome }}</td>
                <td>{{ item.descricao || "-" }}</td>
                <td>{{ item.atualizadoPor || "-" }}</td>
                <td>{{ item.atualizadoEm || "-" }}</td>
                <td>
                  <v-menu>
                    <template v-slot:activator="{ on, attrs }">
                      <v-btn color="gray" dark v-bind="attrs" v-on="on">
                        Opções
                        <v-icon>mdi-menu-down</v-icon>
                      </v-btn>
                    </template>
                    <v-list>
                      <v-list-item link :to="`/projetos/editar/${item.id}`">
                        <v-list-item-title>Editar</v-list-item-title>
                      </v-list-item>
                    </v-list>
                  </v-menu>
                </td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import utils from "../../mixins/utils";
import projetoService from "./../../apis/projetoService";

import PainelAcoesNovo from "../../components/PainelAcoesNovo.vue";

export default {
  components: { PainelAcoesNovo },
  mixins: [utils],
  data() {
    return {
      resultado: [],
    };
  },
  methods: {
    cadastrar() {
      this.$router.push({ name: "projetoCadastrar" })
    },
  },
  async created() {
    this.resultado = await projetoService.list(this.lerToken());
  },
};
</script>