
<template>
  <v-content>
    <v-container fluid fill-height>
      <v-layout align-center justify-center>
        <v-flex xs12 sm8 md4>
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
              <v-btn color="primary" @click="entrar">Login</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </v-content>
</template>

<script>
export default {
    data() {
        return {
            email: '',
            senha: ''
        }
    },
    methods: {
        entrar() {
            this.$router.push({ name: 'home' });
        }
    }
}
</script>