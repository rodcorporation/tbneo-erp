
<template>
  <v-main>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
          <v-alert v-model="exibirAlerta" color="red" dismissible dark>{{
            mensagem
          }}</v-alert>
          <v-card class="elevation-12">
            <v-toolbar dark color="dark">
              <v-toolbar-title>TbNeo - ERP</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field
                  prepend-icon="mdi-account"
                  name="login"
                  label="Login"
                  type="text"
                  v-model="email"
                ></v-text-field>
                <v-text-field
                  id="password"
                  prepend-icon="mdi-key-chain-variant"
                  name="password"
                  label="Password"
                  type="password"
                  v-model="senha"
                ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="primary"
                :loading="loading"
                @click="entrar"
                >Login</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-main>
</template>

<script>
import utils from "../../mixins/utils";
import usuarioService from "../../apis/usuarioService";

export default {
  mixins: [utils],
  data() {
    return {
      email: "felipe.souza@gmail.com",
      senha: "",
      mensagem: "Usuário ou senha inválidos.",
      exibirAlerta: false,
      loading: false
    };
  },
  methods: {
    async entrar() {
      const model = {
        email: this.email,
        senha: this.senha,
      };

      this.loading = true;

      const response = await usuarioService.login(model);

      if (response && response.token) {
        this.salvarToken(response.token);
        this.$router.push({ path: "/home" });
      } else {
        this.mensagem = response[0];
      }

      this.loading = false;
    },
  },
  watch: {
    mensagem() {
      this.exibirAlerta = true;
    },
  },
};
</script>