package com.example.cipher2.controllers

import com.example.cipher2.ProjectValues.ip
import com.example.cipher2.models.TextModel

interface ICipherRequest {

    fun decryptUrl(key: String, format: String): String =
        "http://$ip/WebApplication228/api/Decrypt?key=$key&format=$format"

    fun encryptUrl(key: String, format: String): String =
        "http://$ip/WebApplication228/api/Encrypt?key=$key&format=$format"

    fun getTextModelResponse(): TextModel
    suspend fun sendEncrypt()
    suspend fun sendDecrypt()
}