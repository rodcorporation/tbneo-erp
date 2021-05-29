<template>
  <v-app id="inspire">

    <v-app-bar app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-btn text>Relacionamento</v-btn>
      <v-btn text>Operações</v-btn>
      <v-btn text>Faturamento</v-btn>
      <v-btn text>Segurança</v-btn>
      <v-btn text>T.I.</v-btn>

      <v-spacer></v-spacer>

      <v-btn icon>
        <v-icon>mdi-bell</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-format-list-bulleted</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-account-circle</v-icon>
      </v-btn>
    </v-app-bar>

    <v-navigation-drawer dark v-model="drawer" app>
      <v-list-item class="grey lighten-4" style="max-height:64px;">
        <v-list-item-content>
          <v-list-item-avatar>
            <v-img :src="require('@/assets/logo.png')"></v-img>
          </v-list-item-avatar>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>

      <v-list nav>
        <v-list-item>
          <v-list-item-subtitle>Parametrizações</v-list-item-subtitle>
        </v-list-item>
        <v-list-item link>
          <v-list-item-icon
            ><v-icon>mdi-code-greater-than</v-icon></v-list-item-icon
          >
          <v-list-item-content>Geral</v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-subtitle>Feature Flags</v-list-item-subtitle>
        </v-list-item>
        <v-list-item link>
          <v-list-item-icon
            ><v-icon>mdi-code-greater-than</v-icon></v-list-item-icon
          >
          <v-list-item-content>Infra</v-list-item-content>
        </v-list-item>
        <v-list-item link>
          <v-list-item-icon
            ><v-icon>mdi-code-greater-than</v-icon></v-list-item-icon
          >
          <v-list-item-content>Demais Sistemas</v-list-item-content>
        </v-list-item>
        <v-list-item link to="/projetos">
          <v-list-item-icon><v-icon>mdi-code-greater-than</v-icon></v-list-item-icon>
          <v-list-item-content>Projetos</v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-subtitle>Acessos</v-list-item-subtitle>
        </v-list-item>
        <v-list-item link>
          <v-list-item-icon
            ><v-icon>mdi-code-greater-than</v-icon></v-list-item-icon
          >
          <v-list-item-content>Acesso</v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      drawer: true,
      items: [
        { title: "Home", icon: "mdi-home-city" },
        { title: "My Account", icon: "mdi-account" },
        { title: "Users", icon: "mdi-account-group-outline" },
      ],
    };
  },
};
</script>