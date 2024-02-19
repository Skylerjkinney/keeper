import { AppState } from "../AppState"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"


class VaultsService {
    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        logger.log('vaulting vault', response.data)
        const newVault = new Vault(response.data)
        AppState.vaults.unshift(newVault)
        return newVault
    }
}

export const vaultsService = new VaultsService()