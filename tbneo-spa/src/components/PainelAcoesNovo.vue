<template>
    <v-card class="ma-5">
      <v-card-actions>
        <v-btn color="success" @click="novo">Novo</v-btn>
      </v-card-actions>
    </v-card>    
</template>

<script>
export default {
    methods: {
        novo() {
            this.$emit('novo');
        }
    }
}
</script>