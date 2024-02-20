import { AppState } from "../AppState"
import { Profile } from "../models/Profile"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfileService {
    async getProfileById(profileId) {
        const response = await api.get(`api/profiles/${profileId}`)
        logger.log('Got Profile in service layer', response.data)
        const newProfile = new Profile(response.data)
        AppState.activeProfile = newProfile
    }
}
export const profileService = new ProfileService()