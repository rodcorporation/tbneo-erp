<template>
    <v-card class="ma-5">
      <v-card-actions>
        <v-btn color="success" @click="botaoConfirmar">Confirmar</v-btn>
        <v-spacer></v-spacer>
        <v-btn color="danger" @click="botaoCancelar">Cancelar</v-btn>
      </v-card-actions>
    </v-card>    
</template>

<script>
export default {
    methods: {
        botaoConfirmar() {
            this.$emit('confirmar');
        },
        botaoCancelar() {
            this.$emit('cancelar');
        }
    }
}
</script>