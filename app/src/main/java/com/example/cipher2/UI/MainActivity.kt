package com.example.cipher2.UI

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.cipher2.ProjectValues.FILE_PICKER_REQUEST_CODE
import com.example.cipher2.ProjectValues.defaultKey
import com.example.cipher2.ProjectValues.fileUriToUpload
import com.example.cipher2.R
import com.example.cipher2.controllers.DocxCipherRequest
import com.example.cipher2.controllers.ICipherRequest
import com.example.cipher2.controllers.StringCipherRequest
import com.example.cipher2.controllers.TxtCipherRequest
import com.example.cipher2.models.TextModel
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.MainScope
import kotlinx.coroutines.launch


class MainActivity : AppCompatActivity() {
    var model: TextModel = TextModel("", "", "")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        switchMode.isChecked = true
        switchDefaultKey.isChecked = true
        editTextKey.isEnabled = !switchDefaultKey.isChecked
        formatRadioGroup.check(stringRadioButton.id)
        buttonUploadFile.isEnabled = false
        sourceEitText.isEnabled = !buttonUploadFile.isEnabled

        formatRadioGroup.setOnCheckedChangeListener { _, checkedId ->
            when (checkedId) {
                docxRadioButton.id -> buttonUploadFile.isEnabled = true
                txtRadioButton.id -> buttonUploadFile.isEnabled = true
                stringRadioButton.id -> buttonUploadFile.isEnabled = false
            }
            sourceEitText.isEnabled = !buttonUploadFile.isEnabled
        }

            switchMode.setOnCheckedChangeListener { _, isChecked ->
                if (isChecked) {
                    switchMode.text = resources.getText(R.string.title_decrypt)
                    buttonSendRequest.text = resources.getText(R.string.send_decrypt)
                } else {
                    switchMode.text = resources.getText(R.string.title_encrypt)
                    buttonSendRequest.text = resources.getText(R.string.send_encrypt)
                }
            }

            switchDefaultKey.setOnCheckedChangeListener { _, isChecked ->
                editTextKey.isEnabled = !isChecked
            }

            buttonUploadFile.setOnClickListener {
                pickFile()
            }

        buttonSendRequest.setOnClickListener {
            sendRequest()
        }
    }

    override fun onActivityResult(
        requestCode: Int, resultCode: Int,
        resultData: Intent?
    ) {
        super.onActivityResult(requestCode, resultCode, resultData)
        when (requestCode) {
            FILE_PICKER_REQUEST_CODE -> fileUriToUpload = resultData?.data

        }
    }

    private fun sendRequest() {
        buttonSendRequest.isEnabled = false
        var request: ICipherRequest? = null
        val key =
            if (switchDefaultKey.isChecked)
                defaultKey
            else editTextKey.text.toString()

        when (formatRadioGroup.checkedRadioButtonId) {
            docxRadioButton.id -> {
                val inStream = contentResolver?.openInputStream(fileUriToUpload!!)
                request = DocxCipherRequest(key, inStream)
            }
            txtRadioButton.id -> {
                val inStream = contentResolver?.openInputStream(fileUriToUpload!!)
                request = TxtCipherRequest(key, inStream)
            }
            stringRadioButton.id -> {
                val text = sourceEitText.text.toString()
                request = StringCipherRequest(key, text)
            }
        }


        GlobalScope.launch {
            if (switchMode.isChecked) {
                request?.sendDecrypt()
            } else {
                request?.sendEncrypt()
            }
            model = request!!.response
            MainScope().launch {
                SuccessResultDialog(
                    this@MainActivity,
                    model
                ).show()
                buttonSendRequest.isEnabled = true
            }

        }
    }

    private fun pickFile() {
        val intent = Intent(Intent.ACTION_OPEN_DOCUMENT)
        intent.addCategory(Intent.CATEGORY_OPENABLE)
        intent.type = "*/*"
        startActivityForResult(intent, FILE_PICKER_REQUEST_CODE)
    }


}
